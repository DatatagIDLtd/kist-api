using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kist_api.Helper;
using kist_api.Realtime;
using kist_api.Services;
using kist_api.Services.RealtimeService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace kist_api
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
       
            services.AddSingleton(Configuration);
            services.AddHttpClient<IKistService, KistService>();
            services.AddHttpClient<IScanService, ScanService>();
            services.AddHttpClient<IVehicleCheckService, VehicleCheckService>();
            services.AddHttpClient<IAuditService, AuditService>();
            services.AddHttpClient<IContractService, ContractService>();
            services.AddHttpClient<IAuditService, AuditService>();
           
            services.AddScoped<IRealtimeService, RealtimeService>();

            services.AddControllers().AddNewtonsoftJson();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.SetIsOriginAllowed((host) => true)
                     .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });

                //options.AddPolicy("SignalR",
                //    builder => builder
                //    .AllowAnyMethod()
                //    .AllowAnyHeader()
                //    .AllowCredentials()
                //    .WithOrigins("http://localhost:4200"));
                    //.SetIsOriginAllowed(hostName => true)) ;

            }); // Make sure you call this previous to AddMvc
            services.AddControllers();

            services.AddSignalR();

            //   services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    options.Authority = "{yourAuthorizationServerAddress}";
            //    options.Audience = "{yourAudience}";
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddFile(configuration.GetValue<string>("Logging:Location"));

            app.UseHttpsRedirection();

            app.UseRouting();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
          
            app.UseCors("AllowAll");
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<AssetHub>("/assetsHub");
            });
        
        }
    }
}
