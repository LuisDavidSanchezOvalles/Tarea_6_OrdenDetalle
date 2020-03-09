using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RegistroOrdenDetalle.Entidades;
using RegistroOrdenDetalle.BLL;

namespace RegistroOrdenDetalle.UI.Registros
{
    /// <summary>
    /// Interaction logic for ROrden.xaml
    /// </summary>
    public partial class ROrden : Window
    {
        Contenedor contenedor = new Contenedor();
        public ROrden()
        {
            InitializeComponent();
            this.DataContext = contenedor;
            OrdenIdTextBox.Text = "0";
            ClienteIdTextBox.Text = "0";
            ProductoIdTextBox.Text = "0";
            PrecioTextBox.Text = "0";
            CantidadTextBox.Text = "0";
            MontoTextBox.Text = "0";
            FechaDatePicker.SelectedDate = DateTime.Now;
        }

        private void Recargar()
        {
            this.DataContext = null;
            this.DataContext = contenedor;
        }

        private void Limpiar()
        {
            OrdenIdTextBox.Text = "0";
            ClienteIdTextBox.Text = "0";
            NombresClienteTextBox.Text = string.Empty;
            ProductoIdTextBox.Text = "0";
            DescripcionTextBox.Text = string.Empty;
            FechaDatePicker.SelectedDate = DateTime.Now;
            PrecioTextBox.Text = "0";
            CantidadTextBox.Text = "0";
            MontoTextBox.Text = "0";
            contenedor.orden.OrdenesDetalle = new List<OrdenDetalle>();
            contenedor = new Contenedor();
            Recargar();
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Orden OrdenAnterior = OrdenesBLL.Buscar(contenedor.orden.OrdenId);

            return OrdenAnterior != null;
        }

        private bool ExisteEnLaBaseDeDatosClientes()
        {
            Cliente ClienteAnterior = ClientesBLL.Buscar(Convert.ToInt32(ClienteIdTextBox.Text));

            return ClienteAnterior != null;
        }

        private bool ExisteEnLaBaseDeDatosProductos()
        {
            Producto ProductoAnterior = ProductosBLL.Buscar(Convert.ToInt32(ProductoIdTextBox.Text));

            return ProductoAnterior != null;
        }

        private bool Validar()
        {
            bool paso = true;

            if (contenedor.orden.OrdenesDetalle.Count == 0)
                paso = false;

            return paso;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;
            if (!Validar())
                return;

            if (contenedor.orden.OrdenId == 0 && ExisteEnLaBaseDeDatosClientes())
                paso = OrdenesBLL.Guardar(contenedor.orden);
            else
            {
                if (ExisteEnLaBaseDeDatos() && ExisteEnLaBaseDeDatosClientes())
                {
                    paso = OrdenesBLL.Modificar(contenedor.orden);
                }
                else
                {
                    MessageBox.Show("No se Puede Modificar una Orden que no existe, no exista un producto o cliente");
                    return;
                }
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado");
            }
            else
            {
                MessageBox.Show("La Orden No se Pudo Guardar");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdenesBLL.Eliminar(contenedor.orden.OrdenId))
            {
                MessageBox.Show("Eliminado");
                Limpiar();
            }
            else
                MessageBox.Show("No se pudo eliminar una persona que no existe");
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Orden OrdenAnterior = OrdenesBLL.Buscar(contenedor.orden.OrdenId);

            if (OrdenAnterior != null)
            {
                contenedor.orden = OrdenAnterior;
                Recargar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("Orden no encontrada");
            }
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ExisteEnLaBaseDeDatosProductos())
                return;
            contenedor.orden.OrdenesDetalle.Add(new OrdenDetalle(contenedor.orden.OrdenId, Convert.ToInt32(ProductoIdTextBox.Text), 
                DescripcionTextBox.Text, Convert.ToInt32(CantidadTextBox.Text), Convert.ToDecimal(PrecioTextBox.Text),
                Convert.ToDecimal(MontoTextBox.Text)));

            Recargar();

            ProductoIdTextBox.Clear();
            DescripcionTextBox.Clear();
            PrecioTextBox.Clear();
            CantidadTextBox.Clear();
            MontoTextBox.Clear();
            ProductoIdTextBox.Focus();
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdenDataGrid.Items.Count > 1 && OrdenDataGrid.SelectedIndex < OrdenDataGrid.Items.Count - 1)
            {
                contenedor.orden.OrdenesDetalle.RemoveAt(OrdenDataGrid.SelectedIndex);
                Recargar();
            }
        }

        private void LlenaCampoCliente(Cliente cliente)
        {
            NombresClienteTextBox.Text = cliente.Nombres;
        }

        private void ClienteIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(ClienteIdTextBox.Text))
            {
                int id;
                Cliente cliente = new Cliente();
                int.TryParse(ClienteIdTextBox.Text, out id);

                cliente = ClientesBLL.Buscar(id);
                if (cliente != null)
                {
                    LlenaCampoCliente(cliente);
                }
                else
                {
                    NombresClienteTextBox.Text="No existe Tal Cliente";
                }
            }
        }

        private void LlenaCampoProducto(Producto producto)
        {
            DescripcionTextBox.Text = producto.NombreProducto;
            PrecioTextBox.Text = Convert.ToString(producto.Precio);
        }

        private void ProductoIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ProductoIdTextBox.Text))
            {
                int id;
                Producto producto = new Producto();
                int.TryParse(ProductoIdTextBox.Text, out id);

                producto = ProductosBLL.Buscar(id);
                if (producto != null)
                {
                    LlenaCampoProducto(producto);
                }
                else
                {
                    DescripcionTextBox.Text = "No existe Tal Producto";
                    PrecioTextBox.Text = "No Existe Precio";
                }
            }
        }

        private void CantidadTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(CantidadTextBox.Text))
            {
                foreach (var caracter in CantidadTextBox.Text)
                {
                    if (!Char.IsDigit(caracter))
                    {
                        contenedor.detalle.Cantidad = 0;
                        CantidadTextBox.Clear();
                    }
                    else
                    {
                        PrecioTextBox.Text = Convert.ToString(contenedor.detalle.Precio);
                        CantidadTextBox.Text = Convert.ToString(contenedor.detalle.Cantidad);

                        contenedor.detalle.Monto = contenedor.detalle.Precio * contenedor.detalle.Cantidad;

                        MontoTextBox.Text = Convert.ToString(contenedor.detalle.Monto);
                    }
                }
            }
        }
    }
}
