using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class FacturaRequest
    {
        public TblFacturasDTO Factura { get; set; }
        public List<TblDetallesFacturaDTO> DetallesFactura { get; set; }

        public TblClientesDTO Cliente { get; set; }
        public List<CatProductosDTO> Productos { get; set; }
    }
}
