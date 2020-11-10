
using MassTransit;

using MassTut.Commons.Repositories;
using MassTut.Keeper.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MassTut.Keeper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var config = hostContext.Configuration;
                    var busSettings = config
                        .GetSection(BusSettings.SettingsKey)
                        .Get<BusSettings>();
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<LocationEventHandler>();
                        x.SetKebabCaseEndpointNameFormatter();
                        x.UsingRabbitMq((ctx, cfg) =>
                        {
                            cfg.Host(busSettings.Host, busSettings.VirtualHost, h =>
                            {
                                h.Username(busSettings.Username);
                                h.Password(busSettings.Password);
                            });
                            cfg.ConfigureEndpoints(ctx);
                        });                   
                    });
                    services.AddMassTransitHostedService();

                    services.AddSingleton<ILocationRepository, LocationState>();
                    services.AddHostedService<PublisherWorker>();
                });
    }
}
