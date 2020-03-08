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
    public class ClientesBLL
    {
        public static bool Guardar(Cliente cliente)
        {
            Contexto db = new Contexto();
            bool paso = false;

            try
            {
                if (db.Clientes.Add(cliente) != null)
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

        public static bool Modificar(Cliente cliente)
        {
            Contexto db = new Contexto();
            bool paso = false;

            try
            {
                db.Entry(cliente).State = EntityState.Modified;
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
                var eliminar = ClientesBLL.Buscar(id);
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

        public static Cliente Buscar(int id)
        {
            Contexto db = new Contexto();
            Cliente cliente = new Cliente();

            try
            {
                cliente = db.Clientes.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return cliente;
        }

        public static List<Cliente> GetList(Expression<Func<Cliente, bool>> cliente)
        {
            Contexto db = new Contexto();
            List<Cliente> Lista = new List<Cliente>();

            try
            {
                Lista = db.Clientes.Where(cliente).ToList();
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
