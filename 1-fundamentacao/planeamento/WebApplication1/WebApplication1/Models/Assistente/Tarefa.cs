using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Assistente
{
    public class Tarefa
    {
        private int id;
        private string descricao;
        private Dictionary<string, Ingrediente> ingredientes;
        private Dictionary<string, Utensilio> utensilios;
        private Dictionary<string, Tecnica> tecnicas;
        private Dictionary<string, WordLink> conceitos;
    }
}
