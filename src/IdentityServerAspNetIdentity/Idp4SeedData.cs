using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity
{
    public class Idp4SeedData
    {
        public static void EnsureSeedData(IWebHost host, string connectionString)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                    Console.WriteLine("Clients are created");
                }
                else
                {
                    Console.WriteLine("Clients already exist");
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                    Console.WriteLine("IdResources are created");
                }
                else
                {
                    Console.WriteLine("IdResources already exist");
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApis())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                    Console.WriteLine("ApiResources are created");
                }
                else
                {
                    Console.WriteLine("ApiResources already exist");
                }
            }
        }
    }
}
