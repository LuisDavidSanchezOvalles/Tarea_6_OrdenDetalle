using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RegistroOrdenDetalle.Entidades
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        public string Nombres { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }

        [ForeignKey("ClienteId")]
        public virtual List<Orden> OrdenesList { get; set; }

        public Cliente()
        {
            ClienteId = 0;
            Nombres = string.Empty;
            Cedula = string.Empty;
            Telefono = string.Empty;

            OrdenesList = new List<Orden>(); 
        }
    }
}
