using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Entidades
{
    public class Contacto
    {
        public int CompaniaId { get; set; }
        public Compania Compania { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RowId { get; set; }
        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string Direccion { get; set; }
        [StringLength(20)]
        public string Telefono { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public CentroOperacion CentroOperacion { get; set; }

    }
}
