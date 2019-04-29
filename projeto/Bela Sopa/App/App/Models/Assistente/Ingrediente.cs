using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Assistente{
    public class Ingrediente{
        [Key]
        public int Id;

        [Required]
        [StringLength(20)]
        public string Nome;

        [Required]
        [StringLength(200)]
        public string Descricao;

        [Required]
        [StringLength(50)]
        public string ImagePath;

        [Required]
        [StringLength(50)]
        public string Link;
    }
}
