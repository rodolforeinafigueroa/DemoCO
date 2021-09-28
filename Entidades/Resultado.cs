using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Entidades
{
    public class Resultado
    {
        public bool Exito {
            get
            {
                return ListaErrores.Count == 0;
            }
        }
        public List<String> ListaErrores { get; private set; }


        public Resultado()
        {
            ListaErrores = new();
        }

        public void AdicionarErroresAnotaciones(ICollection<ValidationResult> results)
        {
            ListaErrores.AddRange(from result in results
                                  select result.ErrorMessage);
        }

        public void AdicionarError(string Error)
        {
            ListaErrores.Add(Error);
        }

        public string ObtenerMensaje()
        {
            string mensaje;

            if (Exito)
            {
                mensaje = "Proceso termino correctamente";
            }
            else
            {
                mensaje = "Proceso termino con errores:";
                mensaje += string.Join("\n* ", ListaErrores);
            }

            return mensaje;
        }
    }
}
