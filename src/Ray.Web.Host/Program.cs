using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ray.Edna.Option;
using Ray.Edna.TaskCore;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using System;
using System.Linq;

namespace Ray.Web.Hosting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBulider(args).Build().Run();
        }

        private static IHostBuilder CreateHostBulider(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                      .ConfigureAppConfiguration((host, opt) =>
                      {
                          opt.SetBasePath(Environment.CurrentDirectory).AddJsonFile("application.json", false, true);
                      }).ConfigureLogging((host, opt) =>
                      {
                          Log.Logger = new LoggerConfiguration().WriteTo
                          .Console(LogEventLevel.Information)
                          .CreateLogger();
                      }).UseSerilog().ConfigureServices((host, opt) =>
                      {

                          AppOption.Jobs = host.Configuration.GetSection("Jobs").GetChildren()
                          .Select(t => t.GetChildren().FirstOrDefault())
                          .ToDictionary(t => t.Key, t => t.Value);

                          opt.AddSingleton<QuartzService>();
                          opt.AddHostedService<TaskHostService>();
                      }).UseConsoleLifetime();
        }
    }
}
