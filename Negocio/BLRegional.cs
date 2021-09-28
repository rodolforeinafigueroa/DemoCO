using ERP.AccesoDatos;
using ERP.Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Negocio
{
    public class BLRegional
    {
        private ERPContext _context;
        public BLRegional(ERPContext context)
        {
            _context = context;
        }
        public void Adicionar(Regional regional)
        {
            ICollection<ValidationResult> results;
            ValidacionModelo.Validate<Regional>(regional, out results);
            if(results.Count > 0)
            {
                ValidacionModelo.ImprimirErrores(results);
                return;
            }
            _context.Add(regional);
            _context.SaveChanges();
        }

        public Regional Leer(string id)
        {
            var regional = _context.Regionales.Find(id);
            return regional;
        }

        public void Actualizar(Regional regional)
        {
            _context.Update(regional);
            _context.SaveChanges();
        }

        //public Resultado ValidaParaMaestros(string regionalId)
        //{
        //    var regional = _context.Regionales.Find(regionalId);
        //    var resultado = new Resultado();
        //    if(regional.IndEstado == Regional.Estado.Inactivo)
        //    {
        //        resultado.AdicionarError("La regional esta inactiva");
        //    }
        //    return resultado;
        //}
    }
}
