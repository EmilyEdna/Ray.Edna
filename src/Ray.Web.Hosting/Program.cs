using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ray.Edna.Option;
using Ray.Edna.TaskCore;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Synctool.LinqFramework;
using Synctool.StaticFramework;
using System;
using System.Collections.Generic;
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
                          .Console(LogEventLevel.Information, theme: GetTheme())
                          .CreateLogger();
                      }).UseSerilog().ConfigureServices((host, opt) =>
                      {
                          #region 配置
                          AppOption.Jobs = host.Configuration.GetSection("Jobs").GetChildren()
                          .Select(t => t.GetChildren().FirstOrDefault())
                          .ToDictionary(t => t.Key, t => t.Value);


                          AppOption.CookieStr = args.FirstOrDefault(); //host.Configuration.GetSection("CookieStr").Value;

                          AppOption.MoneyDay = Convert.ToInt32(host.Configuration["MoneyDay"]);

                          AppOption.Charge = host.Configuration.GetSection("AutoCharge").GetChildren().Select(t => t.Value).ToList();

                          AppOption.ChargeUp = host.Configuration.GetSection("ChargeUpId").Value;
                          #endregion

                          //opt.AddSingleton<QuartzService>();
                          opt.AddHostedService<TaskHostService>();

                          SyncStatic.Assembly("Ray.Edna.BiliBili")
                          .SelectMany(t => t.ExportedTypes.Where(x => x.GetInterface("IBiliBili") != null))
                          .ForEnumerEach(item =>
                          {
                              BiliBiliOption.Set(item.Name, Activator.CreateInstance(item));
                          });

                      }).UseConsoleLifetime();
        }

        private static SystemConsoleTheme GetTheme()
        {
            var dic = new Dictionary<ConsoleThemeStyle, SystemConsoleThemeStyle>
            {
                {ConsoleThemeStyle.Text, new SystemConsoleThemeStyle{ Foreground= ConsoleColor.White} },
                 {ConsoleThemeStyle.String, new SystemConsoleThemeStyle{ Foreground= ConsoleColor.Yellow} },
                {ConsoleThemeStyle.Number, new SystemConsoleThemeStyle{ Foreground= ConsoleColor.Magenta} },
                {ConsoleThemeStyle.LevelInformation, new SystemConsoleThemeStyle{ Foreground= ConsoleColor.Green} }
            };
            return new SystemConsoleTheme(dic);
        }
    }
}
