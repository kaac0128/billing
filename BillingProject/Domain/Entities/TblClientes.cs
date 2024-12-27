using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TblClientes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string RazonSocial { get; set; }

        [Required]
        public int IdTipoCliente { get; set; }

        [ForeignKey("IdTipoCliente")]
        public CatTipoCliente TipoCliente { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        [MaxLength(50)]
        public string RFC { get; set; }

        public ICollection<TblFacturas> Facturas { get; set; }
    }
}
