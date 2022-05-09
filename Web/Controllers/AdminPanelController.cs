using Data;
using Data.Domain;
using Data.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using blogDemoCom.Web.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using Rest;

namespace blogDemoCom.Web.Controllers
{
    [Authorize(Roles = ("Admin"))]
    public class AdminPanelController : Controller
    {
        private DataContext dbcontext = new DataContext();
        private readonly ILogger<AdminPanelController> _logger;

        public AdminPanelController(ILogger<AdminPanelController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index(string searchText = "", int page = 1)
        {
            List<Post> posts = dbcontext.Post.Where(x => x.Status == true).OrderByDescending(x => x.CreateTime).ToList(); 
            User user = GetUser();
            ViewBag.Name = user.FullName;
            if(!String.IsNullOrEmpty(searchText)){
                posts = dbcontext.Post.Where(x => x.Title.Contains(searchText) || x.Content.Contains(searchText)).OrderByDescending(x => x.CreateTime).ToList();                
            }
            
            if (IsAjaxRequest(Request))
                return PartialView("~/Views/Shared/_PostList.cshtml", posts);
            return View(posts);
        }
        public IActionResult Settings()
        {
            User user = GetUser(); 
            ViewBag.Name = user.FullName;           
            return View(user);
        }

        public IActionResult PostAdd()
        {
            User user = GetUser(); 
            ViewBag.Name = user.FullName;           
            return View();
        }

        public IActionResult PostSettings(int page = 1)
        {           
            User user = GetUser();
            ViewBag.Name = user.FullName;
            List<Post> posts = dbcontext.Post.OrderByDescending(x => x.CreateTime).ToList();
            return View(posts);
        }

        [HttpPost]
        public JsonResult Guncelle(string Name, string LastName, string Email, string Password, int Id)
        {
            User userUpdate = dbcontext.User.Where(x => x.Id == Id).FirstOrDefault(); 
            userUpdate.Name = Name;
            userUpdate.LastName = LastName;
            userUpdate.Email = Email;
            userUpdate.Password = Password;
            dbcontext.SaveChanges();        
            return Json(new { status = 1, title = "İşlem Başarılı", message = "Ayarlarınız güncellendi!" });
        }

        [HttpPost]
        public JsonResult PostDelete(int Id)
        {
            Post postDelete = dbcontext.Post.Where(x => x.Id == Id).FirstOrDefault();
            dbcontext.Remove(postDelete);
            dbcontext.SaveChanges();        
            return Json(new { status = 1, title = "İşlem Başarılı", message = "Ayarlarınız güncellendi!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostAdd(Post postVM, IFormFile file)
        {
            try
            {
                if(file != null)
                {                  
                    Post post = new Post()
                    {
                        Title = postVM.Title,
                        Content = postVM.Content,
                        AuthorName = postVM.AuthorName,
                        IsOnline = false
                    };  
                    string filename = "";
                    filename = Guid.NewGuid().ToString() + "-" + post.Id + ".png";  
                    post.Image = filename;                 
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/postImages", filename);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    dbcontext.Add(post);
                    dbcontext.SaveChanges();        
                    return RedirectToAction(nameof(AdminPanelController.PostSettings));
                }
                return RedirectToAction(nameof(AdminPanelController.PostSettings));
                
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return View(postVM);
        }

        public async Task<JsonResult> PostAddApi(Post postVM)
        {
            try
            {
                Post post = new Post()
                {
                    Title = postVM.Title,
                    Content = postVM.Content,
                    Image = postVM.Image,
                    AuthorName = postVM.AuthorName,
                    IsOnline = true 
                };
                
                dbcontext.Add(post);
                dbcontext.SaveChanges();
                return null;

            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return null;
        }

        public async  Task<JsonResult> CheckNewsApi()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("https://gorest.co.in/public/v1/posts?page=1");
                var apiresult = JsonConvert.DeserializeObject<APIResult>(json);
                foreach (var item in apiresult.data.OrderBy(x => Guid.NewGuid()).Take(3))
                {
                    Post post = new Post()
                    {

                        Title = item.title,
                        Content = item.body,
                        Image = "https://profesyoneldeklansor.files.wordpress.com/2014/12/1-3.jpg",
                        AuthorName = "Api"
                    };
                    PostAddApi(post);
                }
            }

            return null;
        }
        public IActionResult PostEdit(int Id){
            User user = GetUser();
            ViewBag.Name = user.FullName; 
            Post postUpdate = dbcontext.Post.Where(x => x.Id == Id).FirstOrDefault();
            return View(postUpdate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostEdit(Post postVM, IFormFile file)
        {
            try
            {
                Post postUpdate = dbcontext.Post.Where(x => x.Id == postVM.Id).FirstOrDefault();            
                string filename = "";
                filename = Guid.NewGuid().ToString() + "-" + postVM.Id + ".png";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/postImages", filename);
                if(file != null){
                    postUpdate.Image = filename;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }                
                postUpdate.Title = postVM.Title;
                postUpdate.Content = postVM.Content;
                postUpdate.UpdateTime = DateTime.Now;  
                postUpdate.AuthorName = postVM.AuthorName;          
                dbcontext.Update(postUpdate);
                dbcontext.SaveChanges();
                return RedirectToAction(nameof(AdminPanelController.PostSettings));                
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Beklenmedik Hata: {ex.Message}";
            }
            return View(postVM);
        }
        public ActionResult PostStatusToggle(int Id)
        {
            Post post = dbcontext.Post.Where(x => x.Id == Id).FirstOrDefault();
            post.Status = !post.Status;
            dbcontext.Update(post);
            dbcontext.SaveChanges(); 
            return Json(new { status = 1, title = "İşlem Başarılı", message = "Ayarlarınız güncellendi!", postStatus = post.Status });
        } 
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/Home/Index");
        }        

        #region ---------User Security
        public User GetUser()
        {
            List<Claim> useridentity = GetIdentitiy();
            if (useridentity == null)
                return null;
            if (useridentity.FirstOrDefault(x => x.Type == "id") == null)
                return null;
            Int16 userid = Convert.ToInt16(useridentity.FirstOrDefault(x => x.Type == "id").Value);
            User user = dbcontext.User.Where(x => x.Id == userid).FirstOrDefault();
            return user;
        }
        public void SetIdentity(User user) 
        {
            var claims = new List<Claim> {                
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim("id",user.Id.ToString())
            };
            var userIdentity = new ClaimsIdentity(claims, "user");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            HttpContext.SignInAsync(principal);

        }
        public List<Claim> GetIdentitiy() 
        {
            List<Claim> checkuser = User.Claims.ToList();
            if (checkuser != null)
                return checkuser;
            return null;
        }
        
        #endregion ----------User Security

        public bool IsAjaxRequest(HttpRequest request)
        {
            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
