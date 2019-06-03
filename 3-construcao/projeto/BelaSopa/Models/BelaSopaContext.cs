using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Models.DomainModels.Utilizadores;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BelaSopa.Models
{
    public class BelaSopaContext : DbContext
    {
        private readonly GestorImagens gestorImagens;

        public BelaSopaContext(
            DbContextOptions<BelaSopaContext> options,
            IHostingEnvironment hostingEnvironment
            ) : base(options)
        {
            gestorImagens = new GestorImagens(hostingEnvironment);
        }

        public virtual DbSet<Administrador> Administrador { get; set; }

        public virtual DbSet<Cliente> Cliente { get; set; }

        public virtual DbSet<ClienteExcluiIngrediente> ClienteExcluiIngrediente { set; get; }

        public virtual DbSet<ClienteReceitaFavorita> ClienteReceitaFavorita { get; set; }

        public virtual DbSet<ClienteReceitaFinalizada> ClienteReceitaFinalizada { get; set; }

        public virtual DbSet<Etiqueta> Etiqueta { get; set; }

        public virtual DbSet<Ingrediente> Ingrediente { get; set; }

        public virtual DbSet<NomeAlternativoIngrediente> NomeAlternativoIngrediente { get; set; }

        public virtual DbSet<NomeAlternativoTecnica> NomeAlternativoTecnica { get; set; }

        public virtual DbSet<NomeAlternativoUtensilio> NomeAlternativoUtensilio { get; set; }

        public virtual DbSet<Processo> Processo { get; set; }

        public virtual DbSet<Receita> Receita { get; set; }

        public virtual DbSet<ReceitaEtiqueta> ReceitaEtiqueta { get; set; }

        public virtual DbSet<RefeicaoEmentaSemanal> RefeicaoEmentaSemanal { get; set; }

        public virtual DbSet<Tarefa> Tarefa { get; set; }

        public virtual DbSet<Tecnica> Tecnica { get; set; }

        public virtual DbSet<TextoTarefa> TextoTarefa { get; set; }

        public virtual DbSet<Utensilio> Utensilio { get; set; }

        public virtual DbSet<UtilizacaoIngrediente> UtilizacaoIngrediente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<ClienteReceitaFavorita>()
                .HasKey(cr => new { cr.ClienteId, cr.ReceitaId });
            modelBuilder.Entity<ClienteReceitaFavorita>()
                .HasOne(ti => ti.Cliente)
                .WithMany(t => t.ClienteFavorito)
                .HasForeignKey(ti => ti.ClienteId);
            modelBuilder.Entity<ClienteReceitaFavorita>()
                .HasOne(ti => ti.Receita)
                .WithMany(i => i.ClienteFavorito)
                .HasForeignKey(ti => ti.ReceitaId);

            modelBuilder.Entity<ClienteReceitaFinalizada>()
                .HasKey(cr => new { cr.ClienteId, cr.ReceitaId });
            modelBuilder.Entity<ClienteReceitaFinalizada>()
                .HasOne(ti => ti.Cliente)
                .WithMany(t => t.ClienteFinalizado)
                .HasForeignKey(ti => ti.ClienteId);
            modelBuilder.Entity<ClienteReceitaFinalizada>()
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

            modelBuilder
                .Entity<RefeicaoEmentaSemanal>()
                .HasKey(x => new { x.ClienteId, x.DiaDaSemana, x.RefeicaoDoDia });
        }

        public Utilizador GetUtilizador(string nomeDeUtilizador)
        {
            return
                this.Cliente.SingleOrDefault(c => c.NomeDeUtilizador == nomeDeUtilizador) as Utilizador ??
                this.Administrador.SingleOrDefault(a => a.NomeDeUtilizador == nomeDeUtilizador) as Utilizador;
        }

        public void AdicionarReceita(Receita receita, IEnumerable<string> nomesEtiquetas, byte[] imagem)
        {
            AdicionarReceitas(new[] { (
                Receita: receita,
                NomesEtiquetas: (ISet<string>)new HashSet<string>(nomesEtiquetas),
                Imagem: imagem
                ) });
        }

        public void AdicionarIngrediente(Ingrediente ingrediente, byte[] imagem)
        {
            AdicionarIngredientesTecnicasUtensilios(new[] { (ingrediente, imagem) }, null, null);
        }

        public void AdicionarTecnica(Tecnica tecnica, byte[] imagem)
        {
            AdicionarIngredientesTecnicasUtensilios(null, new[] { (tecnica, imagem) }, null);
        }

        public void AdicionarUtensilio(Utensilio utensilio, byte[] imagem)
        {
            AdicionarIngredientesTecnicasUtensilios(null, null, new[] { (utensilio, imagem) });
        }

        public void AdicionarReceitas(IList<(Receita Receita, ISet<string> NomesEtiquetas, byte[] Imagem)> receitas)
        {
            // adicionar receitas

            Receita.AddRange(receitas.Select(r => r.Receita));

            // adicionar etiquetas e relacionamentos receita-etiqueta

            var etiquetas = Etiqueta.ToDictionary(e => e.Nome, e => e);

            foreach (var (receita, nomesEtiquetas, _) in receitas)
            {
                foreach (var nomeEtiqueta in nomesEtiquetas)
                {
                    // adicionar etiqueta se não existir

                    var etiqueta = etiquetas.GetValueOrDefault(nomeEtiqueta);

                    if (etiqueta == null)
                    {
                        etiqueta = new Etiqueta { Nome = nomeEtiqueta };
                        Etiqueta.Add(etiqueta);

                        etiquetas.Add(etiqueta.Nome, etiqueta);
                    }

                    // adicionar relacionamento receita-etiqueta

                    var receitaEtiqueta = new ReceitaEtiqueta();

                    receita.ReceitaEtiqueta.Add(receitaEtiqueta);
                    etiqueta.ReceitaEtiqueta.Add(receitaEtiqueta);
                }
            }

            // guardar alterações

            SaveChanges();

            // armazenar imagens

            foreach (var (receita, _, imagem) in receitas)
                gestorImagens.AdicionarImagemReceita(receita.ReceitaId, imagem);

            // descobrir relacionamentos

            var ituPorNome = GetItuPorNome();

            foreach (var (receita, _, _) in receitas)
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
            IList<(Ingrediente Ingrediente, byte[] Imagem)> ingredientes,
            IList<(Tecnica Tecnica, byte[] Imagem)> tecnicas,
            IList<(Utensilio Utensilio, byte[] Imagem)> utensilios
            )
        {
            ingredientes = ingredientes ?? new List<(Ingrediente Ingrediente, byte[] Imagem)>();
            tecnicas = tecnicas ?? new List<(Tecnica Tecnica, byte[] Imagem)>();
            utensilios = utensilios ?? new List<(Utensilio Utensilio, byte[] Imagem)>();

            // adicionar ingredientes, técnicas e utensílios

            Ingrediente.AddRange(ingredientes.Select(i => i.Ingrediente));
            Tecnica.AddRange(tecnicas.Select(t => t.Tecnica));
            Utensilio.AddRange(utensilios.Select(u => u.Utensilio));

            // guardar alterações

            SaveChanges();

            // armazenar imagens

            foreach (var (ingrediente, imagem) in ingredientes)
                gestorImagens.AdicionarImagemIngrediente(ingrediente.IngredienteId, imagem);

            foreach (var (tecnica, imagem) in tecnicas)
                gestorImagens.AdicionarImagemTecnica(tecnica.TecnicaId, imagem);

            foreach (var (utensilio, imagem) in utensilios)
                gestorImagens.AdicionarImagemUtensilio(utensilio.UtensilioId, imagem);

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

                if (ingredientes != null && ingredientes.Count > 0)
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
