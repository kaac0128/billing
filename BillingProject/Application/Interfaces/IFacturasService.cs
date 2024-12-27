using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFacturasService
    {
        Task<IEnumerable<TblFacturasDTO>> GetFacturasAsync(string tipoBusqueda, string valorBusqueda);
        Task<int> CreateAsync(TblFacturasDTO factura, List<TblDetallesFacturaDTO> detallesFactura, TblClientesDTO cliente, List<CatProductosDTO> productos);
    }
}
