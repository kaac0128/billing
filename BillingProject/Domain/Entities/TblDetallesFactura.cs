using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TblDetallesFactura
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdFactura { get; set; }

        [ForeignKey("IdFactura")]
        public TblFacturas Factura { get; set; }

        [Required]
        public int IdProducto { get; set; }

        [ForeignKey("IdProducto")]
        public CatProductos Producto { get; set; }

        [Required]
        public int CantidadDeProducto { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitarioProducto { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubtotalProducto { get; set; }

        [MaxLength(200)]
        public string Notas { get; set; }
    }
}
