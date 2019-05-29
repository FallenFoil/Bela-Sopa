using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Models.DomainModels.Utilizadores;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task AdicionarReceitaAsync(Receita receita, IEnumerable<string> nomesEtiquetas)
        {
            // adicionar receita

            Receita.Add(receita);

            // adicionar etiquetas e relacionamentos receita-etiqueta

            foreach (var nomeEtiqueta in nomesEtiquetas)
            {
                // adicionar etiqueta se não existir

                var etiqueta = Etiqueta.SingleOrDefault(e => e.Nome == nomeEtiqueta);

                if (etiqueta == null)
                    etiqueta = Etiqueta.Add(new Etiqueta { Nome = nomeEtiqueta }).Entity;

                // adicionar relacionamento receita-etiqueta

                var receitaEtiqueta = new ReceitaEtiqueta
                {
                    EtiquetaId = etiqueta.EtiquetaId,
                    Etiqueta = etiqueta,
                    ReceitaId = receita.ReceitaId,
                    Receita = receita
                };

                receita.ReceitaEtiqueta.Add(receitaEtiqueta);
                etiqueta.ReceitaEtiqueta.Add(receitaEtiqueta);

                ReceitaEtiqueta.Add(receitaEtiqueta);
            }

            // guardar alterações

            await SaveChangesAsync();
        }

        public async Task AdicionarIngredienteAsync(Ingrediente ingrediente)
        {

        }

        public DbSet<Administrador> Administrador { get; set; }

        public DbSet<Cliente> Cliente { get; set; }

        //public DbSet<ClienteFinalizado> ClientesFinalizado { get; set; }
        //public DbSet<ClienteEmentaSemanal> ClientesEmentaSemanal { get; set; }
        //public DbSet<ClienteFavorito> ClientesFavorito { get; set; }

        public DbSet<Receita> Receita { set; get; }
        public DbSet<Etiqueta> Etiqueta { set; get; }
        public DbSet<ReceitaEtiqueta> ReceitaEtiqueta { set; get; }
        public DbSet<ReceitaIngrediente> ReceitaIngrediente { set; get; }
        public DbSet<ReceitaProcesso> ReceitaProcesso { set; get; }

        public DbSet<Processo> Processo { set; get; }
        public DbSet<ProcessoTarefa> ProcessoTarefa { set; get; }

        public DbSet<Tarefa> Tarefa { set; get; }
        public DbSet<TarefaIngrediente> TarefaIngrediente { get; set; }
        public DbSet<TarefaUtensilio> TarefaUtensilio { get; set; }
        public DbSet<TarefaTecnica> TarefaTecnica { get; set; }
        public DbSet<Ingrediente> Ingrediente { set; get; }
        public DbSet<Utensilio> Utensilio { set; get; }
        public DbSet<Tecnica> Tecnica { set; get; }

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

            //modelBuilder.Entity<ClienteEmentaSemanal>()
            //    .HasKey(cr => new { cr.ClienteId, cr.ReceitaId });
            //modelBuilder.Entity<ClienteEmentaSemanal>()
            //  .HasOne(ti => ti.Cliente)
            //  .WithMany(t => t.ClienteEmentaSemanal)
            //  .HasForeignKey(ti => ti.ClienteId);
            //modelBuilder.Entity<ClienteEmentaSemanal>()
            //    .HasOne(ti => ti.Receita)
            //    .WithMany(i => i.ClienteEmentaSemanal)
            //    .HasForeignKey(ti => ti.ReceitaId);

            modelBuilder.Entity<TarefaIngrediente>()
                .HasKey(ti => new { ti.TarefaId, ti.IngredienteId });
            modelBuilder.Entity<TarefaIngrediente>()
                .HasOne(ti => ti.Tarefa)
                .WithMany(t => t.TarefaIngrediente)
                .HasForeignKey(ti => ti.TarefaId);
            modelBuilder.Entity<TarefaIngrediente>()
                .HasOne(ti => ti.Ingrediente)
                .WithMany(i => i.TarefaIngrediente)
                .HasForeignKey(ti => ti.IngredienteId);

            modelBuilder.Entity<TarefaUtensilio>()
               .HasKey(ti => new { ti.TarefaId, ti.UtensilioId });
            modelBuilder.Entity<TarefaUtensilio>()
                .HasOne(ti => ti.Tarefa)
                .WithMany(t => t.TarefaUtensilio)
                .HasForeignKey(ti => ti.TarefaId);
            modelBuilder.Entity<TarefaUtensilio>()
                .HasOne(ti => ti.Utensilio)
                .WithMany(i => i.TarefaUtensilio)
                .HasForeignKey(ti => ti.UtensilioId);

            modelBuilder.Entity<TarefaTecnica>()
               .HasKey(ti => new { ti.TarefaId, ti.TecnicaId });
            modelBuilder.Entity<TarefaTecnica>()
                .HasOne(ti => ti.Tarefa)
                .WithMany(t => t.TarefaTecnica)
                .HasForeignKey(ti => ti.TarefaId);
            modelBuilder.Entity<TarefaTecnica>()
                .HasOne(ti => ti.Tecnica)
                .WithMany(i => i.TarefaTecnica)
                .HasForeignKey(ti => ti.TecnicaId);

            modelBuilder.Entity<ProcessoTarefa>()
               .HasKey(pt => new { pt.ProcessoId, pt.TarefaId });
            modelBuilder.Entity<ProcessoTarefa>()
                .HasOne(p => p.Processo)
                .WithMany(t => t.ProcessoTarefa)
                .HasForeignKey(ti => ti.ProcessoId);
            modelBuilder.Entity<ProcessoTarefa>()
                .HasOne(t => t.Tarefa)
                .WithMany(pt => pt.ProcessoTarefa)
                .HasForeignKey(t => t.TarefaId);

            modelBuilder.Entity<ReceitaIngrediente>()
              .HasKey(pt => new { pt.ReceitaId, pt.IngredienteId });
            modelBuilder.Entity<ReceitaIngrediente>()
                .HasOne(ri => ri.Receita)
                .WithMany(r => r.ReceitaIngrediente)
                .HasForeignKey(ti => ti.ReceitaId);
            modelBuilder.Entity<ReceitaIngrediente>()
                .HasOne(ri => ri.Ingrediente)
                .WithMany(i => i.ReceitaIngrediente)
                .HasForeignKey(t => t.IngredienteId);

            modelBuilder.Entity<ReceitaProcesso>()
                .HasKey(rc => new { rc.ReceitaId, rc.ProcessoId });
            modelBuilder.Entity<ReceitaProcesso>()
                .HasOne(ri => ri.Receita)
                .WithMany(i => i.ReceitaProcesso)
                .HasForeignKey(t => t.ReceitaId);
            modelBuilder.Entity<ReceitaProcesso>()
               .HasOne(ri => ri.Processo)
               .WithMany(i => i.ReceitaProcesso)
               .HasForeignKey(t => t.ProcessoId);

            modelBuilder.Entity<ReceitaEtiqueta>()
                .HasKey(rc => new { rc.ReceitaId, rc.EtiquetaId });
            modelBuilder.Entity<ReceitaEtiqueta>()
                .HasOne(ri => ri.Receita)
                .WithMany(i => i.ReceitaEtiqueta)
                .HasForeignKey(t => t.ReceitaId);
            modelBuilder.Entity<ReceitaEtiqueta>()
               .HasOne(ri => ri.Etiqueta)
               .WithMany(i => i.ReceitaEtiqueta)
               .HasForeignKey(t => t.EtiquetaId);
        }
    }
}
