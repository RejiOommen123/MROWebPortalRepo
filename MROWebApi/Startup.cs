using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MRODBL.Entities;
using MROWebApi.Services;
using System;

namespace MROWebApi
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

            try
            {
                var origins = Configuration["AllowedOrigins"].Split(";");
                services.AddCors(c =>
                {
                    c.AddPolicy("AllowOrigin", options => options.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod());
                });

                #region Code for DI Purpose using DBConnection class
                services.AddSingleton<IConfiguration>(Configuration);
                var config = new DBConnectionInfo();
                config.ConnectionString = Configuration.GetConnectionString("myconn");
                services.AddSingleton(config);
                #endregion

                #region Encrypt Decrypt - Don't Change Order
                //services.AddAuthentication();
                //services.AddSingleton<DataProtectionPurposeStrings>();
                #endregion

                services.AddControllers();
                services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
                
            }
            catch (Exception ex)
            {
                //TODO: Force Log the details
                throw ex;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var origins = Configuration["AllowedOrigins"].Split(";");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(options => options.WithOrigins(origins));
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
