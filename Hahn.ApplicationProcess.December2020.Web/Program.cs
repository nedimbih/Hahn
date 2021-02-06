using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Hahn.ApplicationProcess.December2020.Web {
	public class Program {
        public static void Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(new JsonFormatter(), 
                Configuration.GetValue<string>("Serilog:LogFile", "applicant-log.json.log")) //if value is not found in appsettings, the provided default value will be used
                .CreateLogger();

            try {
                Log.Information("Starting web host");
                BuildWebHost(args).Run();
                
            } catch (Exception ex) {
                Log.Fatal(ex, "Host terminated unexpectedly");
            } finally {
                Log.CloseAndFlush();
            } 
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => {
                    logging.AddFilter("System", LogLevel.Error);
                    logging.AddFilter("Microsoft", LogLevel.Information);
				})
                .UseSerilog()
				.UseStartup<Startup>()
				.Build();
	
    public static IConfiguration Configuration { get; } = new ConfigurationBuilder().
        SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true)
        .AddEnvironmentVariables()
        .Build();
    }
}
