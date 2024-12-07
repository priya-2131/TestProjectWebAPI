using DapperCRUDAngular.Abstraction.Services;
using DapperCRUDAngular.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Graph;
using System.IO;
using Microsoft.AspNetCore.Builder;

namespace PostalCodeAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());


                });


    }
}



//var host = new WebHostBuilder()
//                .UseKestrel()
//                .UseContentRoot(Directory.GetCurrentDirectory())
//                .UseWebRoot("www")
//                .UseIISIntegration()
//                .UseStartup<Startup>()
//                .Build();