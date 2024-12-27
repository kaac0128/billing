using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TblFacturasDTO
    {
        public int Id { get; set; }
        public DateTime FechaEmisionFactura { get; set; }
        public int IdCliente { get; set; }
        public string ClienteRazonSocial { get; set; }  
        public int NumeroFactura { get; set; }
        public int NumeroTotalArticulos { get; set; }
        public decimal SubTotalFacturas { get; set; }
        public decimal TotalImpuestos { get; set; }
        public decimal TotalFactura { get; set; }
        public List<TblDetallesFacturaDTO> DetallesFactura { get; set; }
    }
}
