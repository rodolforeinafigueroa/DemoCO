using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Entidades
{
    public class Regional
    {
        [Required]
        public int CompaniaId { get; set; }
        public Compania Compania { get; set; }

        [Key]
        [StringLength(2)]
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

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<CentroOperacion> CentrosOperacion { get; set; }

    }
}
