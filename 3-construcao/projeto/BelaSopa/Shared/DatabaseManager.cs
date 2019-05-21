using BelaSopa.Models;
using BelaSopa.Models.Utilizadores;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Shared
{
    public static class BaseDeDados
    {
        public static void Inicializar(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BelaSopaDbContext>();

                bool databaseExisted =
                    (context.Database.GetService<IDatabaseCreator>() as IRelationalDatabaseCreator).Exists();

                // aplicar migrações pendentes (cria base de dados se não existir)
                context.Database.Migrate();

                if (databaseExisted)
                {
                    // base de dados acabou de ser criada, inserir dados iniciais
                    InserirDadosIniciais(context);
                }
            }
        }

        private static void InserirDadosIniciais(BelaSopaDbContext context)
        {
            // criar conta de administrador

            context.Add(new Administrador(
                Config.DEFAULT_ADMINISTRADOR_NOME,
                Config.DEFAULT_ADMINISTRADOR_PALAVRA_CHAVE
                ));

            // inserir dados de exemplo

            context.AddRange(RecursosEmbutidos.CarregarReceitasDeExemplo());
            context.AddRange(RecursosEmbutidos.CarregarIngredientesDeExemplo());
            
            // guardar alterações

            context.SaveChanges();
        }
    }
}
