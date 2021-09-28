using ERP.AccesoDatos;
using ERP.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Negocio
{
    public class BLCentroOperacion
    {
        private ERPContext _context;
        public BLCentroOperacion(ERPContext context)
        {
            _context = context;
        }
        public void Adicionar(CentroOperacion centroOperacion)
        {
            _context.Add(centroOperacion);
            _context.SaveChanges();
        }

        public CentroOperacion Leer(string id)
        {
            var centroOperacion = _context.CentrosOperacion.Include("Contacto").Where(co => co.Id == id).FirstOrDefault();
            return centroOperacion;
        }

        public IEnumerable<CentroOperacion> LeerTodos()
        {
            var centroOperacion = _context.CentrosOperacion.Include("Contacto");
            return centroOperacion;
        }

        public Resultado Actualizar(CentroOperacion centroOperacion)
        {
            var resultado = ValidarModificar(centroOperacion);
            try
            {
                if (resultado.Exito)
                {
                    _context.Entry(centroOperacion).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.Update(centroOperacion);
                    _context.SaveChanges();
                }
            }
            catch(Microsoft.EntityFrameworkCore.DbUpdateException updateExpeption)
            {
                resultado.AdicionarError("Error al actualizar el registro ver el campo " + updateExpeption.InnerException.Message);
            }
            catch (Exception e)
            {
                resultado.AdicionarError(e.Message + e.InnerException.Message);
            }

            return resultado;
        }

        private Resultado ValidarModificar(CentroOperacion centroOperacion)
        {
            var resultado = new Resultado();
            ICollection<ValidationResult> results;
            ValidacionModelo.Validate<CentroOperacion>(centroOperacion, out results);
            resultado.AdicionarErroresAnotaciones(results);
            return resultado;
        }
    }
}
