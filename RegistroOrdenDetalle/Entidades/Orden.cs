using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RegistroOrdenDetalle.Entidades
{
    public class Orden
    {
        [Key]
        public int OrdenId { get; set; }
        public int ClienteId { get; set; }
        public string NombresCliente { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey("OrdenId")]
        public virtual List<OrdenDetalle> OrdenesDetalle { get; set; }

        public Orden()
        {
            OrdenId = 0;
            ClienteId = 0;
            NombresCliente = string.Empty;
            Fecha = DateTime.Now;

            OrdenesDetalle = new List<OrdenDetalle>();
        }
    }
}
