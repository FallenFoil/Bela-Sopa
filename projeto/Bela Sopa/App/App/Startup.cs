using BelaSopa.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BelaSopa
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add
        // services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // TODO: alterar quando houver uma base de dados
            var connection = @"Server=DESKTOP-54TBH9M\SQLEXPRESS;Database=Bela Sopa;Trusted_Connection=True;ConnectRetryCount=0";

            services.AddDbContext< BelaSopaContext >(
                options => options.UseSqlServer(connection)
                );

            //services.AddAuthentication(
            //    CookieAuthenticationDefaults.AuthenticationScheme
            //    ).AddCookie(
            //    options => { options.LoginPath = "/LoginView/UserLogin/"; }
            //    );
            
            services.AddMvc().SetCompatibilityVersion(
                CompatibilityVersion.Version_2_1
                );
        }

        // This method gets called by the runtime. Use this method to configure
        // the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
        }
    }
}
