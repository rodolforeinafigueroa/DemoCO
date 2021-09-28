using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Entidades
{
    public class CentroOperacion
    {
        public int CompaniaId { get; set; }
        public Compania Compania { get; set; }

        [Required]
        [StringLength(2, ErrorMessage ="La regional debe ser de dos caracteres")]
        public string RegionalId { get; set; }
        public Regional Regional { get; set; }

        [Key]
        [StringLength(3)]
        public string Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Descripcion { get; set; }

        public enum Estado
        {
            Activo,
            Inactivo
        }

        [Required]
        public Estado IndEstado { get; set; }

        public int ContactoRowid { get; set; }
        public Contacto Contacto { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
