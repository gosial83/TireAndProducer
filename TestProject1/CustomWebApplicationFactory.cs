using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TireAndProducer.Library.Interfaces;
using TireAndProducer.Library.Models;
using TireAndProducer.Library.Servives;

namespace TireAndProducerTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddTransient<IProducerService>(serviceProvider =>
                {
                    var tireService = serviceProvider.GetRequiredService<ITireService>();
                    var producerService = new ProducerService(tireService);

                    var initialProducers = new Dictionary<int, Producer>
                    {
                        { 1, new Producer { Id = 1, Name = "Producent ABC", Class = "premium" } },
                        { 2, new Producer { Id = 2, Name = "Producent XYZ", Class = "premium" } }
                    };

                    foreach (var producer in initialProducers.Values)
                    {
                        producerService.Add(producer);
                    }

                    return producerService;
                });
            });
        }
    }
}
