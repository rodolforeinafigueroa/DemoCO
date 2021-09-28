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
            if (!resultado.Exito)
            {
                return resultado;
            }

            try
            {
                _context.Entry(centroOperacion).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.Update(centroOperacion);
                _context.SaveChanges();
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
            if(!resultado.Exito)
            {
                return resultado;
            }

            //Alt 1
            //var resultadoValidacionRegional = ValidacionesRegional(centroOperacion.RegionalId);
            //resultado.AdicionarResultado(resultadoValidacionRegional);

            //Alt 2
            //var resultadoValidacionRegional = RegionalValidator.ValidarParaUsoEnMaestros(_context, centroOperacion.RegionalId);
            //resultado.AdicionarResultado(resultadoValidacionRegional);

            //Alt 3 - FluentValidation
            var validator = new CentroOperacionValidator();

            FluentValidation.Results.ValidationResult result = validator.Validate(centroOperacion);

            foreach (var error in result.Errors)
            {
                resultado.AdicionarError(error.ErrorMessage);
            }


            return resultado;
        }

        //private Resultado ValidacionesRegional(string regionalId)
        //{
        //    var blRegional = new BLRegional(_context);
        //    var resultado = blRegional.ValidaParaMaestros(regionalId);
        //    return resultado;
        //}
    }
}
