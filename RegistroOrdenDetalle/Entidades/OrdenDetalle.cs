using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistroOrdenDetalle.Entidades
{
    public class OrdenDetalle
    {
        [Key]
        public int OrdenDatalleId { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Monto { get; set; }

        public OrdenDetalle()
        {
            OrdenDatalleId = 0;
            OrdenId = 0;
            ProductoId = 0;
            Descripcion = string.Empty;
            Cantidad = 0;
            Precio = 0;
            Monto = 0;
        }

        public OrdenDetalle(int ordenId, int productoId, string descripcion, int cantidad, decimal precio, decimal monto)
        {
            OrdenDatalleId = 0;
            OrdenId = ordenId;
            ProductoId = productoId;
            Descripcion = descripcion;
            Cantidad = cantidad;
            Precio = precio;
            Monto = monto;
        }
    }
}
