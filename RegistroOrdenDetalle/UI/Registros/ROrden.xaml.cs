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
        Orden orden = new Orden();
        
        public ROrden()
        {
            InitializeComponent();
            this.DataContext = orden;
            OrdenIdTextBox.Text = "0";
            FechaDatePicker.SelectedDate = DateTime.Now;
            PrecioTextBox.Text = "0";
        }

        private void Recargar()
        {
            this.DataContext = null;
            this.DataContext = orden;
        }

        private void Limpiar()
        {
            OrdenIdTextBox.Text = "0";
            ClienteIdTextBox.Text = string.Empty;
            NombresClienteTextBox.Text = string.Empty;
            ProductoIdTextBox.Text = string.Empty;
            DescripcionTextBox.Text = string.Empty;
            FechaDatePicker.SelectedDate = DateTime.Now;
            PrecioTextBox.Text = "0";
            CantidadTextBox.Text = string.Empty;
            MontoTextBox.Text = string.Empty;
            orden.OrdenesDetalle = new List<OrdenDetalle>();
            orden = new Orden();
            Recargar();
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Orden OrdenAnterior = OrdenesBLL.Buscar(orden.OrdenId);

            return OrdenAnterior != null;
        }

        private bool ExisteEnLaBaseDeDatosClientes()
        {
            Cliente ClienteAnterior = ClientesBLL.Buscar(orden.ClienteId);

            return ClienteAnterior != null;
        }

        private bool ExisteEnLaBaseDeDatosProductos()
        {
            Producto ProductoAnterior = ProductosBLL.Buscar(Convert.ToInt32(ClienteIdTextBox.Text));

            return ProductoAnterior != null;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (orden.OrdenId == 0)
                paso = OrdenesBLL.Guardar(orden);
            else
            {
                if (ExisteEnLaBaseDeDatos() && ExisteEnLaBaseDeDatosClientes() && ExisteEnLaBaseDeDatosProductos())
                {
                    paso = OrdenesBLL.Modificar(orden);
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
            if (OrdenesBLL.Eliminar(orden.OrdenId))
            {
                MessageBox.Show("Eliminado");
                Limpiar();
            }
            else
                MessageBox.Show("No se pudo eliminar una persona que no existe");
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Orden OrdenAnterior = OrdenesBLL.Buscar(orden.OrdenId);

            if (OrdenAnterior != null)
            {
                orden = OrdenAnterior;
                Recargar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("Persona no encontrada");
            }
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            orden.OrdenesDetalle.Add(new OrdenDetalle(orden.OrdenId, Convert.ToInt32(ProductoIdTextBox.Text), 
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
                orden.OrdenesDetalle.RemoveAt(OrdenDataGrid.SelectedIndex);
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
            decimal Monto, Precio = Convert.ToDecimal(PrecioTextBox.Text);
            int Cantidad = Convert.ToInt32(CantidadTextBox.Text);

            Monto = Precio * Cantidad;

            MontoTextBox.Text = Convert.ToString(Monto);
        }
    }
}
