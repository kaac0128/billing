using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FacturaService : IFacturasService
    {
        private readonly IFacturasRepository _facturasRepository;

        public FacturaService(
            IFacturasRepository facturasRepository)
        {
            _facturasRepository = facturasRepository;
        }

        public async Task<IEnumerable<TblFacturasDTO>> GetFacturasAsync(string tipoBusqueda, string valorBusqueda)
        {
            var facturas = await _facturasRepository.GetFacturasAsync(tipoBusqueda, valorBusqueda);
            return facturas.Select(MapToDto); ;
        }

        public async Task<int> CreateAsync(TblFacturasDTO factura, List<TblDetallesFacturaDTO> detallesFactura, TblClientesDTO cliente, List<CatProductosDTO> productos)
        {
            var facturaId = await _facturasRepository.CreateAsync(MapToEntity(factura), detallesFactura.Select(MapToDetalleFacturaEntity).ToList(), MapToEntity(cliente), productos.Select(MapToProductoEntity).ToList());

            return facturaId;
        }

        private static TblFacturasDTO MapToDto(TblFacturas factura) =>
            new TblFacturasDTO
            {
                Id = factura.Id,
                FechaEmisionFactura = factura.FechaEmisionFactura,
                NumeroFactura = factura.NumeroFactura,
                SubTotalFacturas = factura.SubTotalFacturas,
                TotalImpuestos = factura.TotalImpuestos,
                TotalFactura = factura.TotalFactura
            };

        private static TblFacturas MapToEntity(TblFacturasDTO facturaDto) =>
            new TblFacturas
            {
                FechaEmisionFactura = facturaDto.FechaEmisionFactura,
                NumeroFactura = facturaDto.NumeroFactura,
                SubTotalFacturas = facturaDto.SubTotalFacturas,
                TotalImpuestos = facturaDto.TotalImpuestos,
                TotalFactura = facturaDto.TotalFactura
            };

        private static TblClientes MapToEntity(TblClientesDTO clienteDto) =>
            new TblClientes
            {
                Id = clienteDto.Id,
                RazonSocial = clienteDto.RazonSocial,
                RFC = clienteDto.RFC
            };

        private static TblDetallesFactura MapToDetalleFacturaEntity(TblDetallesFacturaDTO detalleDto) =>
            new TblDetallesFactura
            {
                IdProducto = detalleDto.IdProducto,
                CantidadDeProducto = detalleDto.CantidadDeProducto,
                PrecioUnitarioProducto = detalleDto.PrecioUnitarioProducto,
                SubtotalProducto = detalleDto.SubtotalProducto,
                Notas = detalleDto.Notas
            };

        private static CatProductos MapToProductoEntity(CatProductosDTO productoDto) =>
            new CatProductos
            {
                Id = productoDto.Id,
                PrecioUnitario = productoDto.PrecioUnitario
            };

    }
}
