using BelaSopa.Models.DomainModels.Utilizadores;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class ClienteReceita
    {
        public ClienteReceita() { }
        public ClienteReceita(int idCliente, int idReceita)
        {
            this.ClienteId = idCliente;
            this.ReceitaId = idReceita;
        }
        [Key]
        public int ClienteId { get; set; }
        [Key]
        public int ReceitaId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Receita Receita { get; set; }
    }


    public class ClienteEmentaSemanal
    {
        [Key]
        public int ClienteId { get; set; }
        [Key]
        public int DataRefeicaoId { get; set; }
        [Required]
        public int ReceitaId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Receita Receita { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual DataRefeicao DataRefeicao { get; set; }


        public ClienteEmentaSemanal(int idCliente, int idReceita, int idHorario) {
            this.DataRefeicaoId = idHorario;
            this.ClienteId = idCliente;
            this.ReceitaId = idReceita;
        }

        public ClienteEmentaSemanal() { }
    }

    public class ClienteFavorito : ClienteReceita
    {
        public ClienteFavorito() { }
        public ClienteFavorito(int idCliente, int idReceita) : base(idCliente, idReceita) { }

    }
    public class ClienteFinalizado : ClienteReceita {
        public ClienteFinalizado() { }
        public ClienteFinalizado(int idCliente, int idReceita) : base(idCliente, idReceita) {
            this.Data = DateTime.Now;
        }
        [Required]
        public DateTime Data { get; set; }
    }

    public class ClienteExcluiIngrediente{
        [Required]
        public int ClienteId { set; get; }
        [Required]
        public int IngredienteId { set; get; }

        [NotMapped]
        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Ingrediente Ingrediente { get; set; }
    }


}
