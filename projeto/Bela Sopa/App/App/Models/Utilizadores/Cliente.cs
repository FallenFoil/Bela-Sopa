﻿using App.Models.Assistente;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Utilizadores {
    public class Cliente : Utilizador {
        public Cliente() {
            Localização = "";
        }
        [NotMapped]
        public ICollection<ClienteFavorito> ClienteFavorito { set; get; }
        [NotMapped]
        public ICollection<ClienteFinalizado> ClienteFinalizado { set; get; }
        [NotMapped]
        public ICollection<ClienteEmentaSemanal> ClienteEmentaSemanal { set; get; }
        public string Localização { set; get; }
    }
    
    public abstract class ClienteReceita {
        [Key]
        public int ClienteId { set; get; }
        [Key]
        public int ReceitaId { set; get; }

        [NotMapped]
        [JsonIgnore]
        public virtual Cliente Cliente { set; get; }
        [NotMapped]
        [JsonIgnore]
        public virtual Receita Receita { set; get; }
    }
    
    public class ClienteEmentaSemanal : ClienteReceita{
        [Required]
        public DateTime Horario { set; get; }
    }
    public class ClienteFavorito : ClienteReceita{ }
    public class ClienteFinalizado : ClienteReceita { }
}
