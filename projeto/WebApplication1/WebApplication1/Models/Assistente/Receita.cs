using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.Assistente;

namespace WebApplication1.Models
{
    public class Receita
    {
        private string nome;
        private string dificuldade;
        private int tempo;
        private string descricao;
        private string video;
        private List<String> etiquestas;
        //valores nutricionais
        private int nPessoas;
        private string link;
        private List<Processo> processos;
        private Dictionary<Ingrediente, int> ingredientes;
        private List<Utensilio> utensilio;
        private List<Tecnica> tecnicas;
    }
}
