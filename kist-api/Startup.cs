using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kist_api.Helper;
using kist_api.Services;
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
            services.AddHttpClient<IConsumableService, ConsumableService>();
            services.AddHttpClient<IAuditService, AuditService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            }); // Make sure you call this previous to AddMvc
            services.AddControllers();
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
            });
        
         
        }
    }
}
