using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoTourSiteAdmin.Models;
using GoTourSiteAdmin.Services.Implementation;
using GoTourSiteAdmin.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoTourSiteAdmin {
    public class Startup {
        public static string _vReturnUrl = null;


        public static List<UserViewModel> _vDataUser;
        public  static List<UserPortalViewModel> _vDataUserPortal;
        public static Guid _IdUserPortal;
        public static Guid _IdCity;
        public static Guid _IdCompany;
        //internal static List<CompanyViewModel> _vDataCompany;

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IUserPortalService, UserPortalService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc(options =>
            {
                options.MaxModelValidationErrors = 50;
                options.AllowValidatingTopLevelNodes = true;
            })
    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Login}/{id?}");
            });
        }
    }
}
