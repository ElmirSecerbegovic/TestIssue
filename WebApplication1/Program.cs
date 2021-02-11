using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Web;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Set the NLog manager
            LogManager.LoadConfiguration(string.Concat(System.AppContext.BaseDirectory, "/nlog.config"));

            ILogger logger = LogManager.GetCurrentClassLogger();

            logger.Info("STARTING", 0);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseUrls("http://*:5000");
               webBuilder.UseStartup<Startup>();
               webBuilder.UseNLog();
           });
    }
}
