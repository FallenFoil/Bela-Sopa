using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.Assistente;

namespace WebApplication1.Models
{
    public class Processo
    {
        private int id;
        private int tempo;
        List<Tarefa> tarefas;
    }
}
