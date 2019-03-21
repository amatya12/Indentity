using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityDeepDive
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
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var connectionstring = @"Data Source= DESKTOP-JK3L343\MSSQLSERVER01; database=Ring1; trusted_connection=yes";
          var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
           services.AddDbContext<PluralSightUserDbContext>(options => options.UseSqlServer(connectionstring , sql=> sql.MigrationsAssembly(migrationAssembly)));
            // services.AddIdentityCore<PluralSightUser>(options=> { }); It does not have it cookie;

            services.AddIdentity<PluralSightUser,IdentityRole>(options => {
                options.Tokens.EmailConfirmationTokenProvider = "emailconf";
                options.Password.RequireUppercase = false;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
            })
                                                                    .AddEntityFrameworkStores<PluralSightUserDbContext>()
                                                                    .AddDefaultTokenProviders()
                                                                    .AddTokenProvider<EmailConfirmationTokenProvider<PluralSightUser>>("emailconf")
                                                                    .AddPasswordValidator<DoesNotContainPasswordValidator<PluralSightUser>>();
            // In Addidentity core we can delete the custom cookie because it has it ouwn default cookie. unlike Addidentity core.
           services.AddScoped<IUserStore<PluralSightUser>, UserOnlyStore<PluralSightUser, PluralSightUserDbContext>> ();

            // services.AddAuthentication("cookies").AddCookie("cookies", options => options.LoginPath = "/Home/Login");


            services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(3));
            services.Configure<EmailConfirmationTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(2));
            services.ConfigureApplicationCookie(options => options.LoginPath ="/Home/Login");
            services.AddScoped<IUserClaimsPrincipalFactory<PluralSightUser>, PluralSightUserClaimFactory>();
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
                //app.UseHsts();
            }
            app.UseAuthentication();
           // app.UseHttpsRedirection();
            app.UseStaticFiles();
           // app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
