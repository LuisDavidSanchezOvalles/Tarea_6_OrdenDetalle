﻿using System;
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
    /// Interaction logic for RProducto.xaml
    /// </summary>
    public partial class RProducto : Window
    {
        Producto producto = new Producto();
        public RProducto()
        {
            InitializeComponent();
            this.DataContext = producto;
            ProductoIdTextBox.Text = "0";
        }

        private void Limpiar()
        {
            ProductoIdTextBox.Text = "0";
            NombreProductoTextBox.Text = string.Empty;
            PrecioTextBox.Text = string.Empty;
            
            //Recargar();
        }

        private void Recargar()
        {
            this.DataContext = null;
            this.DataContext = producto;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool Validar()
        {
            bool paso = true;

            if (string.IsNullOrWhiteSpace(ProductoIdTextBox.Text))
                paso = false;
            else
            {
                try
                {
                    int i = Convert.ToInt32(ProductoIdTextBox.Text);
                }
                catch (FormatException)
                {
                    paso = false;
                }
            }

            if (string.IsNullOrWhiteSpace(NombreProductoTextBox.Text))
                paso = false;
            else
            {
                foreach (var caracter in NombreProductoTextBox.Text)
                {
                    if (!char.IsLetter(caracter) && !char.IsWhiteSpace(caracter))
                        paso = false;
                }
            }

            if (string.IsNullOrWhiteSpace(PrecioTextBox.Text))
                paso = false;
            else
            {
                try
                {
                    decimal i = Convert.ToDecimal(PrecioTextBox.Text);
                }
                catch (FormatException)
                {
                    paso = false;
                }
            }

            return paso;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Producto ProductoAnterior = ProductosBLL.Buscar(producto.ProductoId);

            return ProductoAnterior != null;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (!Validar())
                return;

            if (producto.ProductoId == 0)
                paso = ProductosBLL.Guardar(producto);
            else
            {
                if (ExisteEnLaBaseDeDatos())
                {
                    paso = ProductosBLL.Modificar(producto);
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
            if (ProductosBLL.Eliminar(producto.ProductoId))
            {
                MessageBox.Show("Eliminado");
                Limpiar();
            }
            else
                MessageBox.Show("No se pudo eliminar una persona que no existe");
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Producto ProductoAnterior = ProductosBLL.Buscar(producto.ProductoId);

            if (ProductoAnterior != null)
            {
                producto = ProductoAnterior;
                Recargar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("Cliente no encontrado");
            }
        }
    }
}
