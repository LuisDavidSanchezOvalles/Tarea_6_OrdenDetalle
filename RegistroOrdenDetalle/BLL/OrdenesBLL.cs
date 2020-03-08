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
    public class OrdenesBLL
    {
        public static bool Guardar(Orden orden)
        {
            Contexto db = new Contexto();
            bool paso = false;

            try
            {
                if (db.Ordenes.Add(orden) != null)
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

        public static bool Modificar(Orden orden)
        {
            Contexto db = new Contexto();
            bool paso = false;

            try
            {
                db.Database.ExecuteSqlRaw($"Delete FROM OrdenDetalle Where OrdenId = {orden.OrdenId}");

                foreach (var item in orden.OrdenesDetalle)
                {
                    db.Entry(item).State = EntityState.Added;
                }
                db.Entry(orden).State = EntityState.Modified;
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
                var eliminar = OrdenesBLL.Buscar(id);
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

        public static Orden Buscar(int id)
        {
            Contexto db = new Contexto();
            Orden orden = new Orden();

            try
            {
                orden = db.Ordenes.Include(o => o.OrdenesDetalle).Where(o => o.OrdenId == id).SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return orden;
        }

        public static List<Orden> GetList(Expression<Func<Orden, bool>> orden)
        {
            Contexto db = new Contexto();
            List<Orden> Lista = new List<Orden>();

            try
            {
                Lista = db.Ordenes.Where(orden).ToList();
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
