using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using RazorPage1.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace RazorPage1
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
            services.AddRazorPages();

            services.AddDbContext<RazorPageContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("RazorPageContext")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            #region 权限认证测试
            //app.Use(next =>
            //{
            //    return async c =>
            //    {
            //        if (c.Request.Query.ContainsKey("Name"))
            //        {
            //            await next.Invoke(c);
            //        }
            //        else
            //        {
            //            await c.Response.WriteAsync("No Authorization");
            //        }
            //    };
            //});
            #endregion


            #region 测试中间件
            app.Use(
            //middleware1
            next =>
            {
                Console.WriteLine("Middleware 1 out");
                //RequestDelegate1    执行public RequestDelegate Build()方法后，最后返回的就是这个RequestDelegate。在某个地方调用的时候会传递一个context。
                return new RequestDelegate(
                   async context =>
                    {
                        Console.WriteLine("This is Middleware 1 start");
                        await next.Invoke(context);
                        Console.WriteLine("This is Middleware 1 End");
                    });
            }
            );
            app.Use(
            //middle2
            next =>
            {
                Console.WriteLine("Middleware 2 out");
                return new RequestDelegate(
                   async context =>
                   {
                       Console.WriteLine("This is Middleware 2 start");
                       await next.Invoke(context);
                       Console.WriteLine("This is Middleware 2 End");
                   });
            });
            app.Use(
                //传递一个方法过去，返回一个方法
                next =>
                {
                    Console.WriteLine("Middleware 3 out");
                    //这个方法需要返回 RequestDelegate 类型的方法|委托， 这个委托 传一个context，返回一个Task
                    return new RequestDelegate(
                       //这个
                       async context =>
                       {
                           Console.WriteLine("This is Middleware 3 start");
                           await next.Invoke(context);
                           Console.WriteLine("This is Middleware 3 End");
                       });
                }
            );
            #endregion



            if (env.IsDevelopment())
            {
                logger.LogInformation("In Development environment");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //配置静态文件路径
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
