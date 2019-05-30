using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Models.DomainModels.Utilizadores;
using BelaSopa.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BelaSopa.Models
{
    //public class BelaSopa {
    //    public Receita Receita;
    //    public Dictionary<int, Dictionary<int, Boolean>> EstadoConfecao;
    //}

    public class BelaSopaContext : DbContext
    {
        public BelaSopaContext(DbContextOptions<BelaSopaContext> options) : base(options)
        {
        }

        public Utilizador GetUtilizador(string nomeDeUtilizador)
        {
            return
                this.Cliente.SingleOrDefault(c => c.NomeDeUtilizador == nomeDeUtilizador) as Utilizador ??
                this.Administrador.SingleOrDefault(a => a.NomeDeUtilizador == nomeDeUtilizador) as Utilizador;
        }

        public void AdicionarReceita(Receita receita, IEnumerable<string> nomesEtiquetas)
        {
            // adicionar receita

            Receita.Add(receita);

            // descobrir relacionamentos com ingredientes

            var todosIngredientes =
                Ingrediente
                .Include(i => i.Utilizacoes)
                .ToArray();

            foreach (var utilizacaoIngrediente in receita.UtilizacoesIngredientes)
            {
                // tentar encontrar ingrediente com nome semelhante

                var ingrediente =
                    todosIngredientes
                    .FirstOrDefault(i => TextoContemIngrediente(utilizacaoIngrediente.Nome, i));

                if (ingrediente != null)
                    ingrediente.Utilizacoes.Add(utilizacaoIngrediente);
            }

            // adicionar etiquetas e relacionamentos receita-etiqueta

            foreach (var nomeEtiqueta in nomesEtiquetas)
            {
                // adicionar etiqueta se não existir

                var etiqueta =
                    Etiqueta.SingleOrDefault(e => e.Nome == nomeEtiqueta) ??
                    Etiqueta.Add(new Etiqueta { Nome = nomeEtiqueta }).Entity;

                // adicionar relacionamento receita-etiqueta

                var receitaEtiqueta = new ReceitaEtiqueta();

                receita.ReceitaEtiqueta.Add(receitaEtiqueta);
                etiqueta.ReceitaEtiqueta.Add(receitaEtiqueta);
            }

            // guardar alterações

            SaveChanges();
        }

        public void AdicionarIngrediente(Ingrediente ingrediente)
        {
            // adicionar ingrediente

            Ingrediente.Add(ingrediente);

            // descobrir relacionamentos com receitas

            foreach (var receita in Receita.Include(r => r.UtilizacoesIngredientes).ThenInclude(ui => ui.Ingrediente))
            {
                foreach (var utilizacaoIngrediente in receita.UtilizacoesIngredientes)
                {
                    if (utilizacaoIngrediente.Ingrediente == null)
                    {
                        if (TextoContemIngrediente(utilizacaoIngrediente.Nome, ingrediente))
                            ingrediente.Utilizacoes.Add(utilizacaoIngrediente);
                    }
                }
            }

            // guardar alterações

            SaveChanges();
        }

        public void AdicionarTecnica(Tecnica tecnica)
        {
            // TODO: implement
        }

        public void AdicionarUtensilio(Utensilio utensilio)
        {
            // TODO: implement
        }

        private bool TextoContemIngrediente(string texto, Ingrediente ingrediente)
        {
            return Util.FuzzyContains(texto, ingrediente.Nome);
        }

        public DbSet<Administrador> Administrador { get; set; }

        public DbSet<Cliente> Cliente { get; set; }

        //public DbSet<ClienteFinalizado> ClientesFinalizado { get; set; }
        public DbSet<ClienteEmentaSemanal> ClienteEmentaSemanal { get; set; }
        //public DbSet<ClienteFavorito> ClientesFavorito { get; set; }

        public DbSet<Receita> Receita { get; set; }

        public DbSet<ReceitaEtiqueta> ReceitaEtiqueta { get; set; }

        public DbSet<UtilizacaoIngrediente> UtilizacaoIngrediente { get; set; }

        public DbSet<Etiqueta> Etiqueta { get; set; }

        public DbSet<Processo> Processo { get; set; }

        //public DbSet<ProcessoTarefa> ProcessoTarefa { get; set; }

        public DbSet<Tarefa> Tarefa { get; set; }

        public DbSet<Ingrediente> Ingrediente { get; set; }




        //public DbSet<ReceitaProcesso> ReceitaProcesso { get; set; }

        //public DbSet<TarefaIngrediente> TarefaIngrediente { get; set; }
        //public DbSet<TarefaUtensilio> TarefaUtensilio { get; set; }
        //public DbSet<TarefaTecnica> TarefaTecnica { get; set; }
        public DbSet<Utensilio> Utensilio { get; set; }
        public DbSet<Tecnica> Tecnica { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ClienteFinalizado>()
            //    .HasKey(cr => new { cr.ClienteId, cr.ReceitaId });
            //modelBuilder.Entity<ClienteFinalizado>()
            //   .HasOne(ti => ti.Cliente)
            //   .WithMany(t => t.ClienteFinalizado)
            //   .HasForeignKey(ti => ti.ClienteId);
            //modelBuilder.Entity<ClienteFinalizado>()
            //    .HasOne(ti => ti.Receita)
            //    .WithMany(i => i.ClienteFinalizado)
            //    .HasForeignKey(ti => ti.ReceitaId);

            //modelBuilder.Entity<ClienteFavorito>()
            //    .HasKey(cr => new { cr.ClienteId, cr.ReceitaId });
            //modelBuilder.Entity<ClienteFavorito>()
            //  .HasOne(ti => ti.Cliente)
            //  .WithMany(t => t.ClienteFavorito)
            //  .HasForeignKey(ti => ti.ClienteId);
            //modelBuilder.Entity<ClienteFavorito>()
            //    .HasOne(ti => ti.Receita)
            //    .WithMany(i => i.ClienteFavorito)
            //    .HasForeignKey(ti => ti.ReceitaId);

            modelBuilder.Entity<ClienteEmentaSemanal>()
                .HasKey(cr => new { cr.ClienteId, cr.ReceitaId });
            modelBuilder.Entity<ClienteEmentaSemanal>()
              .HasOne(ti => ti.Cliente)
              .WithMany(t => t.ClienteEmentaSemanal)
              .HasForeignKey(ti => ti.ClienteId);
            modelBuilder.Entity<ClienteEmentaSemanal>()
                .HasOne(ti => ti.Receita)
                .WithMany(i => i.ClienteEmentaSemanal)
                .HasForeignKey(ti => ti.ReceitaId);

            //modelBuilder.Entity<TarefaIngrediente>()
            //    .HasKey(ti => new { ti.TarefaId, ti.IngredienteId });
            //modelBuilder.Entity<TarefaIngrediente>()
            //    .HasOne(ti => ti.Tarefa)
            //    .WithMany(t => t.TarefaIngrediente)
            //    .HasForeignKey(ti => ti.TarefaId);
            //modelBuilder.Entity<TarefaIngrediente>()
            //    .HasOne(ti => ti.Ingrediente)
            //    .WithMany(i => i.TarefaIngrediente)
            //    .HasForeignKey(ti => ti.IngredienteId);

            //modelBuilder.Entity<TarefaUtensilio>()
            //   .HasKey(ti => new { ti.TarefaId, ti.UtensilioId });
            //modelBuilder.Entity<TarefaUtensilio>()
            //    .HasOne(ti => ti.Tarefa)
            //    .WithMany(t => t.TarefaUtensilio)
            //    .HasForeignKey(ti => ti.TarefaId);
            //modelBuilder.Entity<TarefaUtensilio>()
            //    .HasOne(ti => ti.Utensilio)
            //    .WithMany(i => i.TarefaUtensilio)
            //    .HasForeignKey(ti => ti.UtensilioId);

            //modelBuilder.Entity<TarefaTecnica>()
            //   .HasKey(ti => new { ti.TarefaId, ti.TecnicaId });
            //modelBuilder.Entity<TarefaTecnica>()
            //    .HasOne(ti => ti.Tarefa)
            //    .WithMany(t => t.TarefaTecnica)
            //    .HasForeignKey(ti => ti.TarefaId);
            //modelBuilder.Entity<TarefaTecnica>()
            //    .HasOne(ti => ti.Tecnica)
            //    .WithMany(i => i.TarefaTecnica)
            //    .HasForeignKey(ti => ti.TecnicaId);

            //modelBuilder.Entity<ReceitaIngrediente>()
            //  .HasKey(pt => new { pt.ReceitaId, pt.IngredienteId });
            //modelBuilder.Entity<ReceitaIngrediente>()
            //    .HasOne(ri => ri.Receita)
            //    .WithMany(r => r.ReceitaIngrediente)
            //    .HasForeignKey(ti => ti.ReceitaId);
            //modelBuilder.Entity<ReceitaIngrediente>()
            //    .HasOne(ri => ri.Ingrediente)
            //    .WithMany(i => i.ReceitaIngrediente)
            //    .HasForeignKey(t => t.IngredienteId);

            modelBuilder.Entity<ReceitaEtiqueta>()
                .HasKey(re => new { re.ReceitaId, re.EtiquetaId });
            modelBuilder.Entity<ReceitaEtiqueta>()
                .HasOne(re => re.Receita)
                .WithMany(r => r.ReceitaEtiqueta)
                .HasForeignKey(re => re.ReceitaId);
            modelBuilder.Entity<ReceitaEtiqueta>()
                .HasOne(re => re.Etiqueta)
                .WithMany(e => e.ReceitaEtiqueta)
                .HasForeignKey(re => re.EtiquetaId);
        }
    }
}
