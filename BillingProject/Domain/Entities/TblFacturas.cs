using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TblFacturas
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime FechaEmisionFactura { get; set; }

        [Required]
        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public TblClientes Cliente { get; set; }

        [Required]
        public int NumeroFactura { get; set; }

        [Required]
        public int NumeroTotalArticulos { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotalFacturas { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalImpuestos { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalFactura { get; set; }

        public ICollection<TblDetallesFactura> DetallesFactura { get; set; }
    }
}
