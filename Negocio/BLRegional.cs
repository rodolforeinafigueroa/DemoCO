using ERP.AccesoDatos;
using ERP.Entidades;
using System;
using System.Collections.Generic;
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
            _context.Add(regional);
            _context.SaveChanges();
        }

        public Regional Leer(string id)
        {
            var regional = _context.Regionales.First();
            return regional;
        }

        public void Actualizar(Regional regional)
        {
            _context.Update(regional);
            _context.SaveChanges();
        }
    }
}
