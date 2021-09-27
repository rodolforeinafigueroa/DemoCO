using ERP.AccesoDatos;
using ERP.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public void Actualizar(CentroOperacion centroOperacion)
        {
            _context.Entry(centroOperacion).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.Update(centroOperacion);
            _context.SaveChanges();
        }
    }
}
