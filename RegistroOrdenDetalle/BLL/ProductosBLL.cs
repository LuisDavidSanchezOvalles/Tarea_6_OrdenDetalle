using System;
using System.Collections.Generic;
using System.Text;
using RegistroOrdenDetalle.DAL;
using RegistroOrdenDetalle.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace RegistroOrdenDetalle.BLL
{
    public class ProductosBLL
    {
        public static bool Guardar(Producto producto)
        {
            Contexto db = new Contexto();
            bool paso = false;

            try
            {
                if (db.Productos.Add(producto) != null)
                    paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Producto producto)
        {
            Contexto db = new Contexto();
            bool paso = false;

            try
            {
                db.Entry(producto).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            Contexto db = new Contexto();
            bool paso = false;

            try
            {
                var eliminar = ProductosBLL.Buscar(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Producto Buscar(int id)
        {
            Contexto db = new Contexto();
            Producto producto = new Producto();

            try
            {
                producto = db.Productos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return producto;
        }

        public static List<Producto> GetList(Expression<Func<Producto, bool>> producto)
        {
            Contexto db = new Contexto();
            List<Producto> Lista = new List<Producto>();

            try
            {
                Lista = db.Productos.Where(producto).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Lista;
        }
    }
}
