using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace OIDC.IdentityMvc
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAuthentication(options =>
                {
                    //默认验证方案
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    //默认token验证失败后的确认验证结果方案
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddOpenIdConnect(options =>
                {
                    //指定远程认证方案的本地登录处理方案
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    //远程认证地址
                    options.Authority = "https://localhost:5000";
                    //Https强制要求标识
                    options.RequireHttpsMetadata = true;
                    //客户端ID
                    options.ClientId = "apiClient";
                    //客户端Secret（支持隐藏模式和授权码模式，密码模式和客户端模式不需要用户登录）
                    options.ClientSecret = "apiSecret";
                    //请求返回id_token以及token
                    options.ResponseType = OpenIdConnectResponseType.IdTokenToken;
                    //令牌保存标识
                    options.SaveTokens = true;
                    //添加访问secret_api域api的权限，用于access_token
                    options.Scope.Add("secret_api.access");
                    //请求授权用户的PhoneModel Claim，随id_token返回
                    options.Scope.Add("company");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}