using System;
using System.Collections.Generic;
using System.Text;

namespace RegistroOrdenDetalle.Entidades
{
    public class Contenedor
    {
        public Orden orden { get; set; }
        public Cliente cliente { get; set; }
        public OrdenDetalle detalle { get; set; }
        public Producto producto { get; set; }

        public Contenedor()
        {
            orden = new Orden();
            cliente = new Cliente();
            detalle = new OrdenDetalle();
            producto = new Producto();
        }
    }
}
