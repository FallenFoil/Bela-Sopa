using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Assistente{
    public class Tecnica{
        [Key]
        public int TecnicaId { set; get; }

        [Required]
        [StringLength(20)]
        public string Nome { set; get; }

        [Required]
        [StringLength(200)]
        public string Descricao { set; get; }

        [Required]
        [StringLength(50)]
        public string ImagePath { set; get; }

        [Required]
        [StringLength(50)]
        public string Link { set; get; }
    }
}
