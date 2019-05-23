using BelaSopa.Models.BusinessModels;
using System.Collections.Generic;

namespace BelaSopa.Models.ViewModels
{
    public class ListaUtilizadoresViewModel
    {
        public List<Administrador> Administradores { get; set; }

        public List<Cliente> Clientes { get; set; }
    }
}
