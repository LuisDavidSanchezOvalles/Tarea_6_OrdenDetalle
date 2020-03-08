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
using RegistroOrdenDetalle.BLL;
using RegistroOrdenDetalle.Entidades;

namespace RegistroOrdenDetalle.UI.Registros
{
    /// <summary>
    /// Interaction logic for RCliente.xaml
    /// </summary>
    public partial class RCliente : Window
    {
        Cliente cliente = new Cliente();
        public RCliente()
        {
            InitializeComponent();
            this.DataContext = cliente;
            ClienteIdTextBox.Text = "0";
        }

        private void Limpiar()
        {
            ClienteIdTextBox.Text = "0";
            NombresTextBox.Text = string.Empty;
            TelefonoTextBox.Text = string.Empty;
            CedulaTextBox.Text = string.Empty;
            //Recargar();
        }

        private void Nuevo_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool Validar()
        {
            bool paso = true;

            if (string.IsNullOrWhiteSpace(ClienteIdTextBox.Text))
                paso = false;
            else
            {
                try
                {
                    int i = Convert.ToInt32(ClienteIdTextBox.Text);
                }
                catch (FormatException)
                {
                    paso = false;
                }
            }

            if (string.IsNullOrWhiteSpace(NombresTextBox.Text))
                paso = false;
            else
            {
                foreach (var caracter in NombresTextBox.Text)
                {
                    if (!char.IsLetter(caracter) && !char.IsWhiteSpace(caracter))
                        paso = false;
                }
            }

            if (string.IsNullOrWhiteSpace(CedulaTextBox.Text))
                paso = false;
            else
            {
                foreach (var caracter in CedulaTextBox.Text)
                {
                    if (!char.IsDigit(caracter))
                        paso = false;
                }
            }

            return paso;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Cliente ClieteAnterior = ClientesBLL.Buscar(cliente.ClienteId);

            return ClieteAnterior != null;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (!Validar())
                return;

            if (cliente.ClienteId == 0)
                paso = ClientesBLL.Guardar(cliente);
            else
            {
                if (ExisteEnLaBaseDeDatos())
                {
                    paso = ClientesBLL.Modificar(cliente);
                }
                else
                {
                    MessageBox.Show("No se Puede Modificar un Cliente que no existe");
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
                MessageBox.Show("El Cliente No se Pudo Guardar");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientesBLL.Eliminar(cliente.ClienteId))
            {
                MessageBox.Show("Eliminado");
                Limpiar();
            }
            else
                MessageBox.Show("No se pudo eliminar una persona que no existe");
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Cliente ClienteAnterior = ClientesBLL.Buscar(cliente.ClienteId);

            if (ClienteAnterior != null)
            {
                cliente = ClienteAnterior;
                Recargar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("Cliente no encontrado");
            }
        }

        private void Recargar()
        {
            this.DataContext = null;
            this.DataContext = cliente;
        }
    }
}
