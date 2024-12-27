using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CatProductos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string NombreProducto { get; set; }

        public byte[] ImagenProducto { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        [MaxLength(5)]
        public string Ext { get; set; }

        public ICollection<TblDetallesFactura> DetallesFactura { get; set; }
    }
}
