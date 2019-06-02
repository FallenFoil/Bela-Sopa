using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Etiqueta
    {
        [Key]
        public int EtiquetaId { get; set; }

        [Required, StringLength(50)]
        public string Nome { get; set; }

        [NotMapped, JsonIgnore]
        public virtual ICollection<ReceitaEtiqueta> ReceitaEtiqueta { get; set; } = new List<ReceitaEtiqueta>();


        public override bool Equals(object etiqueta) {
            Etiqueta eti = etiqueta as Etiqueta;
            if (eti == null) return false;
            if (eti == this) return true;
            if(this.Nome == null || eti.Nome == null) return false;
            return this.Nome.Equals(eti.Nome);
        }

        public override int GetHashCode() {
            return Nome.GetHashCode();
        }
    }
}
