using System;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using StackExchange.Profiling;
using StackExchange.Profiling.SqlFormatters;
using StackExchange.Profiling.Storage;

namespace MiniProfilerDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public GlobalConfigs GlobalConfigs;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            GlobalConfigs = Configuration.Get<GlobalConfigs>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient(typeof(Lazy<>));
            services.AddDbContext<BloggingContext>(options =>
            {
                options.UseSqlite(GlobalConfigs.ConnectionString);
            }, ServiceLifetime.Scoped);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
                    {
                        options.DefaultScheme = "Cookies";
                        options.DefaultChallengeScheme = "oidc";
                    })
                    .AddCookie("Cookies")
                    .AddOpenIdConnect("oidc", options =>
                    {
                        options.Authority = "https://localhost:7001";
                        options.RequireHttpsMetadata = false;

                        options.ClientId = "mvc";
                        options.SaveTokens = true;

                        options.Scope.Add("role");

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            NameClaimType = "name",
                            RoleClaimType = "role"
                        };
                    });

            // MiniProfiler
            services.AddMiniProfiler(options =>
            {
                // All of this is optional. You can simply call .AddMiniProfiler() for all defaults

                // (Optional) Path to use for profiler URLs, default is /mini-profiler-resources
                // /profiler/results-index
                // /profiler/results
                // /profiler/results-list
                options.RouteBasePath = "/profiler";

                // TODO 使用 SqliteStorage 后端接口异常，待解决
                //options.Storage = new SqliteStorage(GlobalConfigs.ConnectionString);
                // TODO 使用 MySqlStorage 前端js异常，待解决
                //options.Storage = new MySqlStorage(GlobalConfigs.ConnectionStringMySql);

                //// (Optional) Control storage
                //// (default is 30 minutes in MemoryCacheStorage)
                //(options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(60);

                // (Optional) Control which SQL formatter to use, InlineFormatter is the default
                options.SqlFormatter = new InlineFormatter();

                //// (Optional) To control authorization, you can use the Func<HttpRequest, bool> options:
                //// (default is everyone can access profilers)
                //options.ResultsAuthorize = request => MyGetUserFunction(request).CanSeeMiniProfiler;
                //options.ResultsListAuthorize = request => MyGetUserFunction(request).CanSeeMiniProfiler;

                //// (Optional)  To control which requests are profiled, use the Func<HttpRequest, bool> option:
                //// (default is everything should be profiled)
                //options.ShouldProfile = request => MyShouldThisBeProfiledFunction(request);

                //// (Optional) Profiles are stored under a user ID, function to get it:
                //// (default is null, since above methods don't use it by default)
                //options.UserIdProvider = request => MyGetUserIdFunction(request);

                //// (Optional) Swap out the entire profiler provider, if you want
                //// (default handles async and works fine for almost all appliations)
                //options.ProfilerProvider = new MyProfilerProvider();

                // (Optional) You can disable "Connection Open()", "Connection Close()" (and async variant) tracking.
                // (defaults to true, and connection opening/closing is tracked)
                options.TrackConnectionOpenClose = true;

                options.PopupRenderPosition = RenderPosition.BottomLeft;
                options.PopupShowTimeWithChildren = true;

                options.ResultsAuthorize = request => request.HttpContext.User.IsInRole("admin");
                options.UserIdProvider = request => request.HttpContext.User.Identity.Name;

            }).AddEntityFramework();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BloggingContext bloggingContext)
        {
            // generate db
            if (bloggingContext.Database.EnsureCreated())
            {
                // generate MiniProfiler tables
                var sqlScripts = new SqliteStorage(GlobalConfigs.ConnectionString).TableCreationScripts;
                bloggingContext.Database.ExecuteSqlCommand(string.Join(Environment.NewLine, sqlScripts));
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            // MiniProfiler
            // The call to app.UseMiniProfiler must come before the call to app.UseMvc
            app.UseMiniProfiler();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
