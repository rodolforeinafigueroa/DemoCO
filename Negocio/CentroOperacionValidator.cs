using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ERP.Entidades;
using ERP.AccesoDatos;

namespace ERP.Negocio
{
    public class CentroOperacionValidator : AbstractValidator<CentroOperacion>
    {
        public CentroOperacionValidator()
        {            
            RuleFor(co => co.RegionalId).Must(ValidarRegional).WithMessage("La regional debe estar activa");
        }

        private bool ValidarRegional(string regionalId)
        {
            var context = new ERPContext();
            return RegionalValidator.EstaActiva(context, regionalId);
        }
    }
}
