using ERP.AccesoDatos;
using ERP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Negocio
{
    public class BLCompania
    {
        private ERPContext _context;
        public BLCompania(ERPContext context)
        {
            _context = context;
        }
        public void Adicionar(Compania compania)
        {
            _context.Add(compania);
            _context.SaveChanges();
        }

        public Compania Leer(int id)
        {
            var compania = _context.Companias.First();
            return compania;
        }

        public void Actualizar(Compania compania)
        {
            _context.Update(compania);
            _context.SaveChanges();
        }
    }
}
