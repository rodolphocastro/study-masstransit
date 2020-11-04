
using MassTransit;

using MassTut.Commons.Repositories;
using MassTut.Keeper.Infrastructure;

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
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<LocationEventHandler>();
                        x.SetKebabCaseEndpointNameFormatter();
                        x.UsingInMemory((ctx, cfg) =>
                        {                            
                            cfg.ConfigureEndpoints(ctx);
                        });
                    });
                    services.AddMassTransitHostedService();

                    services.AddSingleton<ILocationRepository, LocationState>();
                    services.AddHostedService<PublisherWorker>();
                });
    }
}
