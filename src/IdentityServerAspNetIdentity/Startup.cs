// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace IdentityServerAspNetIdentity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlite(Configuration.GetConnectionString("AspNetIdentityConnection"));
                options.EnableSensitiveDataLogging();
                });
                

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationFormats.Clear();
                options.ViewLocationFormats.Add("/UI/Views/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/UI/Views/Shared/{0}.cshtml");
            });

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // default url : something wierd happens here.
                // /Account/Login is supposed to be default but runtime always direct to /Identity/Account/Login
                options.UserInteraction.LoginUrl = "/Account/Login";
                options.UserInteraction.LogoutUrl = "/Account/Logout";
                options.UserInteraction.ConsentUrl = "/Consent/Index";
            })
                .AddConfigurationStore<ConfigurationDbContext>(options =>
                {
                options.ConfigureDbContext = b => {
                    b.UseSqlite(Configuration.GetConnectionString("IdentityServer4Connection"), sql => sql.MigrationsAssembly(migrationsAssembly));
                    b.EnableSensitiveDataLogging();
                };
                    
                })
                .AddOperationalStore<PersistedGrantDbContext>(options =>
                {
                    options.ConfigureDbContext = b =>
                    {
                        b.UseSqlite(Configuration.GetConnectionString("IdentityServer4Connection"), sql => sql.MigrationsAssembly(migrationsAssembly));
                        b.EnableSensitiveDataLogging();
                    };
                        // this enables automatic token cleanup. this is optional.
                        options.EnableTokenCleanup = true;
                    })
                // this configures IdentityServer4 to use the asp.net Identity implementations of "IUserClaimsPrincipalFactory", "IResourceOwnerPasswordValidator", and "IProfileService".
                // it also configures some of asp.net Identity's options for use with IdentityServer (such as claim types to use and authentication cookie settings)
                .AddAspNetIdentity<ApplicationUser>();

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to http://localhost:5000/signin-google
                    options.ClientId = Configuration.GetConnectionString("GoogleClientId");
                    options.ClientSecret = Configuration.GetConnectionString("GoogleClientSecret");
                })
                .AddFacebook(options => 
                {
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                    options.ClientId = Configuration.GetConnectionString("FacebookClientId");
                    options.ClientSecret = Configuration.GetConnectionString("FacebookClientSecret");
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvc(routes =>
            {
                // route: map request url to controller action
                // Home, Index are default values if a request does not include any controller and action name such as "/"
                // Router automatically supplies default values (Home/Index) so "/" => "/Home/Index"
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}