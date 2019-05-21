using BelaSopa.Models;
using BelaSopa.Models.Utilizadores;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace App.Shared
{
    public static class DatabaseManager
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BelaSopaDbContext>();

                bool databaseExisted =
                    (context.Database.GetService<IDatabaseCreator>() as IRelationalDatabaseCreator).Exists();

                // apply pending migrations (creating database if necessary)
                context.Database.Migrate();

                if (databaseExisted)
                {
                    // database didn't exist, populate it
                    Populate(context);
                }
            }
        }

        private static void Populate(BelaSopaDbContext context)
        {
            context.Add<Administrador>(new Administrador(
                Config.DEFAULT_ADMINISTRADOR_NOME,
                Config.DEFAULT_ADMINISTRADOR_PALAVRA_CHAVE
                ));

            context.SaveChanges();
        }
    }
}
