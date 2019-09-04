using System;
using System.Collections.Generic;
using GoTourWeb.Models;
using GoTourWeb.Services.Implementation;
using GoTourWeb.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoTourWeb {
    public class Startup {

        public static MenuViewModel _vDataMenu = new MenuViewModel();
        public static FilterToursViewModel _vFilterTour = new FilterToursViewModel();
        public static ViewLanguajeViewModel _vViewInfo = new ViewLanguajeViewModel();
        public static LanguageMessageViewModel _vViewMessLang = new LanguageMessageViewModel();
        public static int _OrderingTourDefault= 1;
        public static FilterActivityViewModel _vFilterActivity = new FilterActivityViewModel();
        internal static CityViewModel _vDataCityTours;
        public static List<GeneralSearchViewModel> _vGeneralSearch;
        internal static List<CitySearchViewModel> _VDataSearchCity;
        internal static TourViewModel _vDataTourByPass;
        internal static List<TourSearchViewModel> _vDataSearchTour;
        internal static ActivityViewModel _vDataActivityByPass;
        internal static ActivityCompanyViewModel _vDataActivityCompanyByPass;
        public static string _vReturnUrl = null;
        public static Guid _vIdPaymentRegistered = Guid.Empty;
        public static PaymentSearchUserViewModel _vFilterReservation = new PaymentSearchUserViewModel();
        public static FilterActivityViewModel vFilterActivity = new FilterActivityViewModel();
        public static Guid _vIdPayReservationsDetails= Guid.Empty;
        public static int _vPageNumberReservation = 1;
        public static int _vPageNumberTours = 1;
        public static int _vPageNumberActivity = 1;
        public static Guid _vIdCitySelected = Guid.Empty;
        public static Guid _vIdTourSelected = Guid.Empty;
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            //services.Configure<CookiePolicyOptions>(options => {
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ITourService, TourService>();
            services.AddScoped<IGeneralSearchService, GeneralSearchService>(); 
            services.AddCors();
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession(opts => {
                opts.Cookie.Name = ".GoTour.Session";
                opts.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddFacebook(options => {
                options.AppId = "278330192862045";
                options.AppSecret = "4b5fd3279811ac6d6016f7d9c6d4ef25";
            });
            services.Configure<CookiePolicyOptions>(options => {

                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            /*conf mime*/
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".webmanifest"] = "application/manifest+json";
            app.UseStaticFiles(new StaticFileOptions() {
                ContentTypeProvider = provider
            });
            app.UseMvc();
            /*conf mime*/
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

           

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();
            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

           
        }
    }
}
