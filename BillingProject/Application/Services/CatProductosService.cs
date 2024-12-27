using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CatProductosService : ICatProductosService
    {
        private readonly ICatProductosRepository _repository;

        public CatProductosService(ICatProductosRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CatProductosDTO>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();
            return data.Select(MapToDto);
        }

        public async Task<int> CreateAsync(CatProductosDTO producto)
        {
            return await _repository.CreateAsync(MapToEntity(producto));
        }

        public async Task<bool> UpdateAsync(CatProductosDTO producto)
        {
            return await _repository.UpdateAsync(MapToEntity(producto));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static CatProductosDTO MapToDto(CatProductos catProductos) =>
            new CatProductosDTO
            {
                Id = catProductos.Id,
                NombreProducto = catProductos.NombreProducto,
                PrecioUnitario = catProductos.PrecioUnitario,
                Ext = catProductos.Ext,
                ImagenProducto = catProductos.ImagenProducto
            };

        private static CatProductos MapToEntity(CatProductosDTO catProductos) =>
            new CatProductos
            {
                Id = catProductos.Id,
                NombreProducto = catProductos.NombreProducto,
                PrecioUnitario = catProductos.PrecioUnitario,
                Ext = catProductos.Ext,
                ImagenProducto = catProductos.ImagenProducto
            };
    }
}
