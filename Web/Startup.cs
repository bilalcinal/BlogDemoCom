using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace blogDemoCom.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression(opt =>
            {
                opt.Providers.Add<GzipCompressionProvider>();
                opt.EnableForHttps = true;
            });
            services.Configure<GzipCompressionProviderOptions>(options => options.Level =
            CompressionLevel.Fastest);  

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
            services.AddDistributedMemoryCache();
            
            services.AddSession(options => 
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder.SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddAuthorization();            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => { options.AccessDeniedPath = "/Home/Login"; options.LoginPath = "/Home/Login"; options.ExpireTimeSpan = TimeSpan.FromHours(1); });
            string dbConnString = Configuration.GetConnectionString("DevConnection"); 
            services.AddDbContext<Data.DataContext>(options => options.UseSqlServer(dbConnString));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseRouting();            
            app.UseAuthentication();
            app.UseAuthorization();            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }            
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "PostGoruntuleme",
                 pattern: "Home/Post/{id}",
                 defaults: new { controller = "Home", action = "Post" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");                
            });
        }
    }
}