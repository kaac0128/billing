using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CatProductosDTO
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public byte[] ImagenProducto { get; set; }
        public string Ext { get; set; }
    }
}
