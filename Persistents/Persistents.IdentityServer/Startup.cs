using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Persistents.IdentityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            const string connectionString =
                @"Data Source=(LocalDb)\MSSQLLocalDB;database=Test.IdentityServer4.EntityFramework;trusted_connection=yes;";
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddControllers();

            services.AddIdentityServer()
                .AddOperationalStore(options => options.ConfigureDbContext =
                    builder => builder.UseSqlServer(
                        connectionString,
                        sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)))
                .AddConfigurationStore(options => options.ConfigureDbContext =
                    builder => builder.UseSqlServer(
                        connectionString,
                        sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)))
                .AddDeveloperSigningCredential();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}