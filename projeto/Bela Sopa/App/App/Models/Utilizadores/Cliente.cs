using App.Models.Assistente;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Utilizadores {
    public class Cliente : Utilizador {
        public Cliente() {
            Favoritos = new HashSet<Receita>();
            Finalizados = new HashSet<Receita>();
            EmentaSemanal = new Dictionary<DateTime, Receita>();
            Localização = "";
        }
        [NotMapped]
        public ICollection<Receita> Favoritos { set; get; }
        [NotMapped]
        public ICollection<Receita> Finalizados { set; get; }
        [NotMapped]
        public IDictionary<DateTime, Receita> EmentaSemanal { set; get; }
        public string Localização { set; get; }
    }
}
