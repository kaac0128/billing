using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CatTipoClienteService : ICatTipoClienteService
    {
        private readonly ICatTipoClienteRepository _repository;

        public CatTipoClienteService(ICatTipoClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CatTipoClienteDTO>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();
            return data.Select(MapToDto);
        }

        public async Task<int> CreateAsync(CatTipoClienteDTO tipoCliente)
        {
            return await _repository.CreateAsync(MapToEntity(tipoCliente));
        }

        public async Task<bool> UpdateAsync(CatTipoClienteDTO tipoCliente)
        {
            return await _repository.UpdateAsync(MapToEntity(tipoCliente));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static CatTipoClienteDTO MapToDto(CatTipoCliente catTipoCliente) =>
            new CatTipoClienteDTO
            {
                Id = catTipoCliente.Id,
                TipoCliente = catTipoCliente.TipoCliente
            };

        private static CatTipoCliente MapToEntity(CatTipoClienteDTO catTipoCliente) =>
            new CatTipoCliente
            {
                Id = catTipoCliente.Id,
                TipoCliente = catTipoCliente.TipoCliente
            };
    }
}
