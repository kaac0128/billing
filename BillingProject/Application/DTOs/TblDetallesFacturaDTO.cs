using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TblDetallesFacturaDTO
    {
        public int Id { get; set; }
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }  
        public int CantidadDeProducto { get; set; }
        public decimal PrecioUnitarioProducto { get; set; }
        public decimal SubtotalProducto { get; set; }
        public string Notas { get; set; }
    }
}
