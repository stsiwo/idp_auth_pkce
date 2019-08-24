using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using OrderingApi.Config.AOP.ASPFilter;
using OrderingApi.DI;
using OrderingApi.DI.RabbitMQ;
using OrderingApi.Infrastructure.RabbitMQ.Config;
using RabbitMQ.Client;

namespace OrderingApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var currentContext = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => 
                {
                    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddScoped<TestFilter>();

            var myAssembly = typeof(Startup).Assembly;

            // autofac config 
            var containerBuilder = new ContainerBuilder();

            // rmq config
            var rmqHostName = Configuration.GetConnectionString("RabbitMQHomeName");
            var rmqUserName = Configuration.GetConnectionString("RabbitMQUserName");
            var rmqPassword = Configuration.GetConnectionString("RabbitMQPassword");
            var rmqVirtualHost = Configuration.GetConnectionString("RabbitMQVirtualHost");

            // connection factory for publisher
            containerBuilder.Register<ConnectionFactory>(c =>
            {
                return new ConnectionFactory()
                {
                    HostName = rmqHostName, 
                    UserName = rmqUserName, 
                    Password = rmqPassword, 
                    VirtualHost = rmqVirtualHost
                };
            })
                .Keyed<ConnectionFactory>(ConnectionTypeConstants.Publisher)
                .SingleInstance();

            // connection factory for consumer
            containerBuilder.Register<ConnectionFactory>(c =>
            {
                return new ConnectionFactory()
                {
                    HostName = rmqHostName, 
                    UserName = rmqUserName, 
                    Password = rmqPassword, 
                    VirtualHost = rmqVirtualHost
                };
            })
                .Keyed<ConnectionFactory>(ConnectionTypeConstants.Consumer)
                .SingleInstance();
            
            // assembly scanning; don't need to register module individually
            containerBuilder.RegisterAssemblyModules(myAssembly);
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
