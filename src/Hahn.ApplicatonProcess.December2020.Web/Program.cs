using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hahn.ApplicatonProcess.December2020.Domain.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using System.Diagnostics;
using AutofacDataModule = Hahn.ApplicatonProcess.December2020.Data.Helpers;

namespace Hahn.ApplicatonProcess.December2020.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console(new ExceptionAsObjectJsonFormatter(renderMessage: true))
            .CreateLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel();
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConsole();
                logging.AddFilter("Microsoft", LogLevel.Information);
                logging.AddFilter("System", LogLevel.Error);
                logging.SetMinimumLevel(LogLevel.Trace);
            })
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new AutofacContainerModule());
                builder.RegisterModule(new AutofacDataModule.AutofacContainerModule());
            });
    }
}
