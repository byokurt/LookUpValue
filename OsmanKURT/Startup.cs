using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using OsmanKURT.Business;
using OsmanKURT.Business.Contracts;
using OsmanKURT.Cache;
using OsmanKURT.Common;
using OsmanKURT.Data;
using OsmanKURT.Data.Contracts;
using OsmanKURT.Exception;
using OsmanKURT.Log;
using OsmanKURT.Mail;
using OsmanKURT.Token;

namespace OsmanKURT
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Engine
            services.AddScoped<IAccountEngine, AccountEngine>();
            services.AddScoped<ILookUpValueEngine, LookuUpValueEngine>();
            #endregion

            #region Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILookUpValueRepository, LookUpValueRepository>();
            #endregion

            #region Cache
            services.AddSingleton<ICacheManager>(s => new RedisCacheManager(Configuration.GetSection("RedisConnection").GetValue<string>("IpAddress"), Configuration.GetSection("RedisConnection").GetValue<int>("Port")));
            #endregion

            #region Log
            services.AddSingleton<ILogManager, LogManager>();
            #endregion

            #region Token
            services.AddSingleton<IJsonWebToken, JsonWebToken>();

            var sp = services.BuildServiceProvider();
            var jsonWebToken = sp.GetService<IJsonWebToken>();
            void JwtBearer(JwtBearerOptions jwtBearer)
            {
                jwtBearer.TokenValidationParameters = jsonWebToken.TokenValidationParameters;
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearer);
            #endregion

            #region Mail
            services.AddSingleton<IMailManager, MailManager>();
            #endregion

            services.AddDbContext<MainContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IISOptions>(options => { options.ForwardClientCertificate = false; });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            env.ConfigureNLog("nlog.config");
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

            app.UseMiddleware(typeof(ExceptionMiddleware));

            app.UseMvc(routes => { routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}"); });
        }
    }
}
