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

            Add(receita);

            // descobrir relacionamentos com ingredientes

            var todosIngredientes =
                Ingrediente
                .Include(i => i.NomesAlternativos)
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

            // converter texto das tarefas

            var todasTecnicas = Tecnica.Include(t => t.NomesAlternativos).ToArray();
            var todosUtensilios = Utensilio.Include(u => u.NomesAlternativos).ToArray();

            foreach (var processo in receita.Processos)
            {
                foreach (var tarefa in processo.Tarefas)
                {
                    tarefa.Texto = tarefa.Texto.SelectMany(
                        t => ConverterTextoTarefa(t, todosIngredientes, todasTecnicas, todosUtensilios)
                        ).ToList();
                }
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

            Add(ingrediente);

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

            Add(tecnica);
            SaveChanges();
        }

        public void AdicionarUtensilio(Utensilio utensilio)
        {
            // TODO: implement

            Add(utensilio);
            SaveChanges();
        }

        private bool TextoContemIngrediente(string texto, Ingrediente ingrediente)
        {
            var nomesIngrediente =
                ingrediente
                .NomesAlternativos
                .Select(n => n.Nome)
                .Prepend(ingrediente.Nome)
                .ToArray();

            foreach (var palavra in texto.Split())
                if (nomesIngrediente.Any(nome => Util.FuzzyEquals(palavra, nome)))
                    return true;

            return false;
        }

        private IEnumerable<TextoTarefa> ConverterTextoTarefa(
            TextoTarefa texto,
            IList<Ingrediente> ingredientes,
            IList<Tecnica> tecnicas,
            IList<Utensilio> utensilios
            )
        {
            var resultado = new List<TextoTarefa>();
            var listaPalavras = new List<string>();

            void submeterListaPalavras()
            {
                if (listaPalavras.Count > 0)
                {
                    resultado.Add(new TextoTarefa { Texto = string.Join(' ', listaPalavras) });
                    listaPalavras.Clear();
                }
            }

            foreach (var palavra in texto.Texto.Split())
            {
                var ingrediente = ingredientes.FirstOrDefault(i =>
                    i
                    .NomesAlternativos
                    .Select(n => n.Nome)
                    .Prepend(i.Nome)
                    .Any(n => Util.FuzzyEquals(palavra, n))
                    );

                if (ingrediente != null)
                {
                    submeterListaPalavras();
                    resultado.Add(new TextoTarefa { Texto = palavra, Ingrediente = ingrediente });
                    continue;
                }

                var tecnica = tecnicas.FirstOrDefault(t =>
                    t
                    .NomesAlternativos
                    .Select(n => n.Nome)
                    .Prepend(t.Nome)
                    .Any(n => Util.FuzzyEquals(palavra, n))
                    );

                if (tecnica != null)
                {
                    submeterListaPalavras();
                    resultado.Add(new TextoTarefa { Texto = palavra, Tecnica = tecnica });
                    continue;
                }

                var utensilio = utensilios.FirstOrDefault(u =>
                    u
                    .NomesAlternativos
                    .Select(n => n.Nome)
                    .Prepend(u.Nome)
                    .Any(n => Util.FuzzyEquals(palavra, n))
                    );

                if (utensilio != null)
                {
                    submeterListaPalavras();
                    resultado.Add(new TextoTarefa { Texto = palavra, Utensilio = utensilio });
                    continue;
                }

                listaPalavras.Add(palavra);
            }

            submeterListaPalavras();

            return resultado;
        }

        public DbSet<Administrador> Administrador { get; set; }

        public virtual DbSet<Cliente> Cliente { get; set; }

        public virtual DbSet<ClienteEmentaSemanal> ClienteEmentaSemanal { get; set; }

        public virtual DbSet<ClienteFavorito> ClienteFavorito { get; set; }

        public virtual DbSet<ClienteFinalizado> ClienteFinalizado { get; set; }

        public DbSet<DataRefeicao> DataRefeicao { get; set; }

        public DbSet<Etiqueta> Etiqueta { get; set; }

        public DbSet<Ingrediente> Ingrediente { get; set; }

        public DbSet<NomeAlternativoIngrediente> NomeAlternativoIngrediente { get; set; }

        public DbSet<NomeAlternativoTecnica> NomeAlternativoTecnica { get; set; }

        public DbSet<NomeAlternativoUtensilio> NomeAlternativoUtensilio { get; set; }

        public DbSet<Processo> Processo { get; set; }

        public DbSet<Receita> Receita { get; set; }

        public DbSet<ReceitaEtiqueta> ReceitaEtiqueta { get; set; }

        public DbSet<Tarefa> Tarefa { get; set; }

        public DbSet<Tecnica> Tecnica { get; set; }

        public DbSet<TextoTarefa> TextoTarefa { get; set; }

        public DbSet<Utensilio> Utensilio { get; set; }

        public DbSet<UtilizacaoIngrediente> UtilizacaoIngrediente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClienteEmentaSemanal>()
                .HasKey(cr => new { cr.ClienteId, cr.DataRefeicaoId });
            modelBuilder.Entity<ClienteEmentaSemanal>()
              .HasOne(ti => ti.Cliente)
              .WithMany(t => t.ClienteEmentaSemanal)
              .HasForeignKey(ti => ti.ClienteId);
            modelBuilder.Entity<ClienteEmentaSemanal>()
                .HasOne(ti => ti.Receita)
                .WithMany(i => i.ClienteEmentaSemanal)
                .HasForeignKey(ti => ti.ReceitaId);

            modelBuilder.Entity<ClienteFavorito>()
                .HasKey(cr => new { cr.ClienteId, cr.ReceitaId });
            modelBuilder.Entity<ClienteFavorito>()
              .HasOne(ti => ti.Cliente)
              .WithMany(t => t.ClienteFavorito)
              .HasForeignKey(ti => ti.ClienteId);
            modelBuilder.Entity<ClienteFavorito>()
                .HasOne(ti => ti.Receita)
                .WithMany(i => i.ClienteFavorito)
                .HasForeignKey(ti => ti.ReceitaId);

            modelBuilder.Entity<ClienteFinalizado>()
                .HasKey(cr => new { cr.ClienteId, cr.ReceitaId });
            modelBuilder.Entity<ClienteFinalizado>()
               .HasOne(ti => ti.Cliente)
               .WithMany(t => t.ClienteFinalizado)
               .HasForeignKey(ti => ti.ClienteId);
            modelBuilder.Entity<ClienteFinalizado>()
                .HasOne(ti => ti.Receita)
                .WithMany(i => i.ClienteFinalizado)
                .HasForeignKey(ti => ti.ReceitaId);

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
