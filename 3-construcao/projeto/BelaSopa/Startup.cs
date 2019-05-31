using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Utilizadores;
using BelaSopa.Shared;
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

            //services.AddEntityFrameworkProxies();

            services.AddDbContext<BelaSopaContext>(options =>
                options
                    //.UseLazyLoadingProxies()
                    .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"))
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
            app.UseMvc(routes => routes.MapRoute(name: "default", template: "{controller=Autenticacao}/{action=Index}"));
            app.UseStaticFiles();
        }

        private static void InicializarBaseDeDados(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BelaSopaContext>();

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

        private static void PovoarBaseDeDados(BelaSopaContext context)
        {
            // criar conta de administrador

            context.Administrador.Add(new Administrador
            {
                NomeDeUtilizador = "root",
                HashPalavraPasse = Utilizador.ComputarHashPalavraPasse("root")
            });

            // inserir dados de exemplo

            RecursosEmbutidos.CarregarIngredientesDeExemplo(context);
            RecursosEmbutidos.CarregarTecnicasDeExemplo(context);
            RecursosEmbutidos.CarregarUtensiliosDeExemplo(context);
            RecursosEmbutidos.CarregarReceitasDeExemplo(context);
            RecursosEmbutidos.CarregarDataRefeicao(context);

            // guardar alterações

            context.SaveChanges();
        }
    }
}
