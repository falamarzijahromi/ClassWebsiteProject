using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Common.Models;
using Common.Options;
using CompositionRoot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebsiteHttp.Requirements;
using WebsiteHttp.Validators;

namespace WebsiteHttp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
            });

            services.Configure<CustomOptions>(options => { options.Name = "Mehdi"; });

            services.AddIdentity<User, Role>()
                .AddUserValidator<UserNationalCodeValidator>();
            //.AddPasswordValidator<PasswordValidator>();

            services.AddTransient<IAuthorizationHandler, TimePolicyAuthorizationHandler>();

            services.AddAuthorization(config =>
            {
                config.AddPolicy("OnlyHasTime", polconf =>
                {
                    polconf.RequireAuthenticatedUser();
                    polconf.RequireClaim("Time");
                    polconf.AddRequirements(new TimePolicyRequirement("12:05"));
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            Composition.RegisterDependencies(builder);
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                //routes.MapRoute(
                //    name: "ApplicationRoute",
                //    template: "{action}/{controller}/{*catchAllData}",
                //    defaults: new { controller = "Home", action = "Index"});
            });


        }
    }
}
