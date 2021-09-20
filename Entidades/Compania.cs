using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Entidades
{
    public class Compania
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string RazonSocial { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<Regional> Regionales { get; set; }
        public ICollection<CentroOperacion> CentrosOperacion { get; set; }
        public ICollection<Contacto> Contactos { get; set; }
    }
}
