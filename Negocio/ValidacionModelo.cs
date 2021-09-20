using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Negocio
{
    public static class ValidacionModelo
    {
        public static bool Validate<T>(T obj, out ICollection<ValidationResult> results)
        {
            results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
        }

        public static void ImprimirErrores(ICollection<ValidationResult> results)
        {
            foreach(var result in results)
            {
                Console.WriteLine(result.ErrorMessage); 
            }
        }
    }
}
