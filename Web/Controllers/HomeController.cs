using Data;
using Data.Domain;
using Data.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using blogDemoCom.Web.Models;

using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Newtonsoft.Json;

namespace blogDemoCom.Web.Controllers
{
    public class HomeController : Controller
    {
        private DataContext dbcontext = new DataContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index(string filter = "")
        {
            List<Post> posts = dbcontext.Post.Where(x => x.Status == true).OrderByDescending(x => x.CreateTime).ToList();
            if(!String.IsNullOrEmpty(filter)){
                posts = dbcontext.Post.Where(x => x.Title.Contains(filter) || x.Content.Contains(filter)).OrderByDescending(x => x.CreateTime).ToList();                
            }
            if (IsAjaxRequest(Request))
                return PartialView("~/Views/Shared/_PostList.cshtml", posts);
            return View(posts);
        }
        
        public IActionResult BlogItem(int id)
        {
            Post post = dbcontext.Post.Where(x => x.Id == id).FirstOrDefault();
            return View(post);
        }

        // [HttpGet]
        public IActionResult Login()
        {
            bool isLogged = HttpContext.User != null && HttpContext.User.Identity.IsAuthenticated;
            if(isLogged == true){
                return Redirect("/AdminPanel/Index");
            }
            return View();
        }

        [Route("/login/")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User userVM)
        {
            var user = dbcontext.User.Where(x => x.Email == userVM.Email && x.Password == userVM.Password).FirstOrDefault();
            if(user != null){                
                SetIdentity(user);
                return Redirect("/AdminPanel/Index");
            }
            return View();
        }
        
        // public IActionResult Search(string filter)
        // {
        //     List<Post> posts = dbcontext.Post.OrderByDescending(x => x.CreateTime).ToList();
        //     if (!String.IsNullOrEmpty(filter))
        //     {
        //         posts.Where(x => x.Title.Contains(filter)).ToList();
        //     }

        //     return View(posts);
        // }
        
        

        
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
            var userIdentity = new ClaimsIdentity(claims, "Login");
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
