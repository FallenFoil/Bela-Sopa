using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Utilizadores {
    public abstract class Utilizador {
        [Key]
        public int UtilizadorId { set; get; }
        [Required]
        public string Nome { set; get; }
        [Required]
        public string Email { set; get; }
        [Required]
        public string Distrito { set; get; }
        [Required]
        public string Password { set; get; }
    }
}
