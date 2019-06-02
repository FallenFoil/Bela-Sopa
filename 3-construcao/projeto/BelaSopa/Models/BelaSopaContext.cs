using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Models.DomainModels.Utilizadores;
using BelaSopa.Shared;
using Microsoft.EntityFrameworkCore;
using System;
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

        public virtual DbSet<Administrador> Administrador { get; set; }

        public virtual DbSet<Cliente> Cliente { get; set; }

        public virtual DbSet<ClienteEmentaSemanal> ClienteEmentaSemanal { get; set; }

        public virtual DbSet<ClienteFavorito> ClienteFavorito { get; set; }

        public virtual DbSet<ClienteFinalizado> ClienteFinalizado { get; set; }

        public virtual DbSet<ClienteExcluiIngrediente> ClienteExcluiIngrediente { set; get; }

        public virtual DbSet<DataRefeicao> DataRefeicao { get; set; }

        public virtual DbSet<Etiqueta> Etiqueta { get; set; }

        public virtual DbSet<Ingrediente> Ingrediente { get; set; }

        public virtual DbSet<NomeAlternativoIngrediente> NomeAlternativoIngrediente { get; set; }

        public virtual DbSet<NomeAlternativoTecnica> NomeAlternativoTecnica { get; set; }

        public virtual DbSet<NomeAlternativoUtensilio> NomeAlternativoUtensilio { get; set; }

        public virtual DbSet<Processo> Processo { get; set; }

        public virtual DbSet<Receita> Receita { get; set; }

        public virtual DbSet<ReceitaEtiqueta> ReceitaEtiqueta { get; set; }

        public virtual DbSet<Tarefa> Tarefa { get; set; }

        public virtual DbSet<Tecnica> Tecnica { get; set; }

        public virtual DbSet<TextoTarefa> TextoTarefa { get; set; }

        public virtual DbSet<Utensilio> Utensilio { get; set; }

        public virtual DbSet<UtilizacaoIngrediente> UtilizacaoIngrediente { get; set; }

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

            modelBuilder.Entity<ClienteExcluiIngrediente>()
                .HasKey(cr => new { cr.ClienteId, cr.IngredienteId });
            modelBuilder.Entity<ClienteExcluiIngrediente>()
                .HasOne(ti => ti.Cliente)
                .WithMany(t => t.ClienteExcluiIngrediente)
                .HasForeignKey(ti => ti.ClienteId);
            modelBuilder.Entity<ClienteExcluiIngrediente>()
                .HasOne(ti => ti.Ingrediente)
                .WithMany(i => i.ClienteExcluiIngrediente)
                .HasForeignKey(ti => ti.IngredienteId);

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

        public Utilizador GetUtilizador(string nomeDeUtilizador)
        {
            return
                this.Cliente.SingleOrDefault(c => c.NomeDeUtilizador == nomeDeUtilizador) as Utilizador ??
                this.Administrador.SingleOrDefault(a => a.NomeDeUtilizador == nomeDeUtilizador) as Utilizador;
        }

        public void AdicionarReceita(Receita receita, IEnumerable<string> nomesEtiquetas)
        {
            AdicionarReceitas(new[] {
                (Receita: receita, NomesEtiquetas: (ISet<string>)new HashSet<string>(nomesEtiquetas))
            });
        }

        public void AdicionarIngrediente(Ingrediente ingrediente)
        {
            AdicionarIngredientesTecnicasUtensilios(new[] { ingrediente }, new Tecnica[0], new Utensilio[0]);
        }

        public void AdicionarTecnica(Tecnica tecnica)
        {
            AdicionarIngredientesTecnicasUtensilios(new Ingrediente[0], new[] { tecnica }, new Utensilio[0]);
        }

        public void AdicionarUtensilio(Utensilio utensilio)
        {
            AdicionarIngredientesTecnicasUtensilios(new Ingrediente[0], new Tecnica[0], new[] { utensilio });
        }

        public void AdicionarReceitas(IList<(Receita Receita, ISet<string> NomesEtiquetas)> receitas)
        {
            // adicionar receitas

            Receita.AddRange(receitas.Select(r => r.Receita));

            // adicionar etiquetas e relacionamentos receita-etiqueta

            var etiquetas = Etiqueta.ToDictionary(e => e.Nome, e => e);

            foreach (var (receita, nomesEtiquetas) in receitas)
            {
                foreach (var nomeEtiqueta in nomesEtiquetas)
                {
                    // adicionar etiqueta se não existir

                    var etiqueta = etiquetas.GetValueOrDefault(nomeEtiqueta);

                    if (etiqueta == null)
                    {
                        etiqueta = new Etiqueta { Nome = nomeEtiqueta };
                        etiquetas.Add(etiqueta.Nome, etiqueta);
                    }

                    // adicionar relacionamento receita-etiqueta

                    var receitaEtiqueta = new ReceitaEtiqueta();

                    receita.ReceitaEtiqueta.Add(receitaEtiqueta);
                    etiqueta.ReceitaEtiqueta.Add(receitaEtiqueta);
                }
            }

            Etiqueta.AddRange(etiquetas.Values);

            // guardar alterações

            SaveChanges();

            // descobrir relacionamentos

            var ituPorNome = GetItuPorNome();

            foreach (var (receita, _) in receitas)
            {
                // descobrir relacionamentos da lista de ingredientes com ingredientes

                AtualizarUtilizacoesIngredientesReceita(receita, ituPorNome);

                // descobrir relacionamentos das tarefas com ingredientes, técnicas e utensílios

                AtualizarTextoReceita(receita, ituPorNome);
            }

            // guardar alterações

            SaveChanges();
        }

        public void AdicionarIngredientesTecnicasUtensilios(
            IList<Ingrediente> ingredientes,
            IList<Tecnica> tecnicas,
            IList<Utensilio> utensilios
            )
        {
            // adicionar ingredientes, técnicas e utensílios

            Ingrediente.AddRange(ingredientes);
            Tecnica.AddRange(tecnicas);
            Utensilio.AddRange(utensilios);

            // guardar alterações

            SaveChanges();

            // descobrir relacionamentos

            var receitas =
                Receita
                .Include(r => r.UtilizacoesIngredientes)
                    .ThenInclude(ui => ui.Ingrediente)
                .Include(r => r.Processos)
                    .ThenInclude(p => p.Tarefas)
                    .ThenInclude(t => t.Texto);

            Func<string, object> ituPorNome = null;

            foreach (var receita in receitas)
            {
                ituPorNome = ituPorNome ?? GetItuPorNome();

                // descobrir relacionamentos da lista de ingredientes com ingredientes

                if (ingredientes.Count > 0)
                    AtualizarUtilizacoesIngredientesReceita(receita, ituPorNome);

                // descobrir relacionamentos das tarefas com ingredientes, técnicas e utensílios

                AtualizarTextoReceita(receita, ituPorNome);
            }

            // guardar alterações

            if (ituPorNome != null)
                SaveChanges();
        }

        private void AtualizarUtilizacoesIngredientesReceita(Receita receita, Func<string, object> ituPorNome)
        {
            foreach (var utilizacaoIngrediente in receita.UtilizacoesIngredientes)
            {
                utilizacaoIngrediente
                    .Nome
                    .Split()
                    .Select(palavra => ituPorNome(palavra) as Ingrediente)
                    .FirstOrDefault(i => i != null)
                    ?.Utilizacoes
                    ?.Add(utilizacaoIngrediente);
            }
        }

        private void AtualizarTextoReceita(Receita receita, Func<string, object> ituPorNome)
        {
            foreach (var processo in receita.Processos.OrderBy(p => p.Indice))
                foreach (var tarefa in processo.Tarefas.OrderBy(t => t.Indice))
                    AtualizarTextoTarefa(tarefa, ituPorNome);
        }

        private void AtualizarTextoTarefa(Tarefa tarefa, Func<string, object> ituPorNome)
        {
            var novoTexto = new List<TextoTarefa>();
            var listaPalavras = new List<string>();

            void submeterListaPalavras()
            {
                if (listaPalavras.Count > 0)
                {
                    novoTexto.Add(new TextoTarefa { Indice = novoTexto.Count, Texto = string.Join(' ', listaPalavras) });
                    listaPalavras.Clear();
                }
            }

            foreach (var palavra in string.Join(' ', tarefa.Texto.OrderBy(t => t.Indice).Select(t => t.Texto)).Split())
            {
                switch (ituPorNome(palavra))
                {
                    case Ingrediente ingrediente:
                        submeterListaPalavras();
                        novoTexto.Add(new TextoTarefa { Indice = novoTexto.Count, Texto = palavra, Ingrediente = ingrediente });
                        break;

                    case Tecnica tecnica:
                        submeterListaPalavras();
                        novoTexto.Add(new TextoTarefa { Indice = novoTexto.Count, Texto = palavra, Tecnica = tecnica });
                        break;

                    case Utensilio utensilio:
                        submeterListaPalavras();
                        novoTexto.Add(new TextoTarefa { Indice = novoTexto.Count, Texto = palavra, Utensilio = utensilio });
                        break;

                    default:
                        listaPalavras.Add(palavra);
                        break;
                }
            }

            submeterListaPalavras();

            tarefa.Texto = novoTexto;
        }

        private Func<string, object> GetItuPorNome()
        {
            var ingredientesPorNome =
                Ingrediente
                .Include(i => i.NomesAlternativos)
                .Include(i => i.Utilizacoes)
                .SelectMany(i => i.NomesAlternativos.Select(n => Tuple.Create(n.Nome, (object)i)))
                .ToArray()
                .Union(Ingrediente.Select(i => Tuple.Create(i.Nome, (object)i)))
                .ToArray();

            var tecnicasPorNome =
                Tecnica
                .Include(t => t.NomesAlternativos)
                .SelectMany(t => t.NomesAlternativos.Select(n => Tuple.Create(n.Nome, (object)t)))
                .ToArray()
                .Union(Tecnica.Select(t => Tuple.Create(t.Nome, (object)t)))
                .ToArray();

            var utensiliosPorNome =
                Utensilio
                .Include(u => u.NomesAlternativos)
                .SelectMany(u => u.NomesAlternativos.Select(n => Tuple.Create(n.Nome, (object)u)))
                .ToArray()
                .Union(Utensilio.Select(u => Tuple.Create(u.Nome, (object)u)))
                .ToArray();

            var ituPorNome =
                ingredientesPorNome
                .Concat(tecnicasPorNome)
                .Concat(utensiliosPorNome)
                .ToLookup(t => t.Item1, t => t.Item2)
                .ToDictionary(g => g.Key, g => g.First());

            return nome =>
            {
                foreach (var pair in ituPorNome)
                    if (Util.FuzzyEquals(nome, pair.Key))
                        return pair.Value;

                return null;
            };
        }
    }
}
