using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RazorPage1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                            .ConfigureLogging((context, loggingBuilder) =>
                            {
                                loggingBuilder.AddFilter("System", LogLevel.Warning);//���˵������ռ�
                                loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);
                                loggingBuilder.AddLog4Net();//ʹ��log4net
                            })//��չ��־ 
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
