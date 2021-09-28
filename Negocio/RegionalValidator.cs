using ERP.AccesoDatos;
using ERP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Negocio
{
    public static class RegionalValidator
    {
        public static Resultado ValidarParaUsoEnMaestros(ERPContext context,  string regionalId)
        {
            var blRegional = new BLRegional(context);
            Regional regional = blRegional.Leer(regionalId);
            var resultado = new Resultado();
            if (regional.IndEstado == Regional.Estado.Inactivo)
            {
                resultado.AdicionarError("La regional esta inactiva");
            }
            return resultado;
        }

        public static bool EstaActiva(ERPContext context, string regionalId)
        {
            var blRegional = new BLRegional(context);
            Regional regional = blRegional.Leer(regionalId);

            return regional.IndEstado == Regional.Estado.Activo;
        }
    }
}
