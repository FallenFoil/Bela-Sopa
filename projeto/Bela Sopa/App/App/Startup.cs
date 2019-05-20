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
            services.AddDbContext< BelaSopaContext >(
                options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"))
                );

            // TODO: fix/implement this
            //services.AddAuthentication(
            //    CookieAuthenticationDefaults.AuthenticationScheme
            //    ).AddCookie(
            //    options => { options.LoginPath = "/LoginView/UserLogin/"; }
            //    );
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure
        // the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                serviceScope.ServiceProvider.GetRequiredService<BelaSopaContext>().Database.Migrate();

            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
        }
    }
}
