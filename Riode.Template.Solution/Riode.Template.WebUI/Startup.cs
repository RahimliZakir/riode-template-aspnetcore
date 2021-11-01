using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Riode.Template.WebUI.AppCode.BinderProviders;
using Riode.Template.WebUI.AppCode.Extensions;
using Riode.Template.WebUI.Models.DataContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI
{
    public class Startup
    {
        private readonly IConfiguration conf;

        public Startup(IConfiguration conf)
        {
            this.conf = conf;

            //string text = "salam";

            //string chiper = text.Encrypt();

            //text = chiper.Decrypt();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                //options.LowercaseQueryStrings = true;
            });

            services.AddControllersWithViews(cfg =>
            {
                cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());
            });

            services.AddDbContext<RiodeDbContext>(options =>
            {
                options.UseSqlServer(conf.GetConnectionString("cString"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //Auto DataSeed
            app.DataSeed().Wait();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/comingsoon.html", async (context) =>
                {
                    using StreamReader sr = new StreamReader("views/static/coming-soon.html");
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync(sr.ReadToEnd());
                });

                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(name: "BlogRoute",
                    pattern: "blog.html",
                    defaults: new
                    {
                        action = "Index",
                        controller = "Blog"
                    });

                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
