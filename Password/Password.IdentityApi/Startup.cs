using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Password.IdentityApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddAuthentication("Bearer")
                .AddJwtBearer(r =>
                {
                    //认证地址
                    r.Authority = "http://localhost:5000";
                    //权限标识
                    r.Audience = "secret_api";
                    //是否必需HTTPS
                    r.RequireHttpsMetadata = false;
                    //设置TOKEN有效偏移时间(AccessToken进行验证的时候，会有一个时间偏移)
                    r.TokenValidationParameters.ClockSkew = TimeSpan.FromSeconds(0);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}