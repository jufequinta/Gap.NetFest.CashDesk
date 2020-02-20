﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Gap.NetFest.CashDesk
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await BuildHost(args).RunConsoleAsync();
        }

        public static IHostBuilder BuildHost(string[] args)
       => new HostBuilder()
                .ConfigureAppConfiguration((hostContext, config) =>
                {

                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                    if (string.IsNullOrEmpty(environment))
                    {
                        System.Console.WriteLine("INFO: Environment variable not setted (ASPNETCORE_ENVIRONMENT)");
                        System.Console.WriteLine("Default Environment: Development");
                        environment = "Development";
                    }
                    config.AddEnvironmentVariables();
                    config.AddJsonFile($"appsettings.{environment}.json", optional: true);
                    config.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<HostedService>();
                });
    }
}
