using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFacturasRepository
    {
        Task<IEnumerable<TblFacturas>> GetFacturasAsync(string tipoBusqueda, string valorBusqueda);
        Task<int> CreateAsync(TblFacturas factura, List<TblDetallesFactura> detallesFactura, TblClientes cliente, List<CatProductos> productos );
    }
}
