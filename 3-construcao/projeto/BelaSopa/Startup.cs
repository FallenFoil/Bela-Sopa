using BelaSopa.Models;
using BelaSopa.Models.Utilizadores;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
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
            // adicionar base de dados

            services.AddDbContext<BelaSopaDbContext>(
                options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"))
                );

            // configurar autenticação por cookies

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/";
                    options.LoginPath = "/";
                });

            // configurar MVC

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure
        // the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            InicializarBaseDeDados(app);

            app.UseAuthentication();
            app.UseMvc(routes => routes.MapRoute(name: "default", template: "{controller=Entrar}/{action=Index}"));
            app.UseStaticFiles();
        }

        private static void InicializarBaseDeDados(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BelaSopaDbContext>();

                bool databaseExisted =
                    (context.Database.GetService<IDatabaseCreator>() as IRelationalDatabaseCreator).Exists();

                // aplicar migrações pendentes (cria base de dados se não existir)
                context.Database.Migrate();

                if (!databaseExisted)
                {
                    // base de dados acabou de ser criada, realizar povoamento inicial
                    PovoarBaseDeDados(context);
                }
            }
        }

        private static void PovoarBaseDeDados(BelaSopaDbContext context)
        {
            // criar conta de administrador

            context.Add(new Administrador
            {
                NomeDeUtilizador = "root",
                HashPalavraPasse = Credenciais.ComputarHashPalavraPasse("root")
            });

            // inserir dados de exemplo

            //context.AddRange(RecursosEmbutidos.CarregarReceitasDeExemplo());
            //context.AddRange(RecursosEmbutidos.CarregarIngredientesDeExemplo());

            // guardar alterações

            context.SaveChanges();
        }
    }
}