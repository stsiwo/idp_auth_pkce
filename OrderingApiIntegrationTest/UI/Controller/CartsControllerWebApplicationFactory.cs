using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using OrderingApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApiIntegrationTest.UI.Controller
{
    public class CartsControllerWebApplicationFactory : WebApplicationFactory<Startup> 
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .BuildServiceProvider();

                // Build the service provider.
                var sp = services.BuildServiceProvider();
            });
        } 
    }
}
