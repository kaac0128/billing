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
    public class TblClientesService : ITblClientesService
    {
        private readonly ITblClientesRepository _repository;

        public TblClientesService(ITblClientesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TblClientesDTO>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();
            return data.Select(MapToDto);
        }

        public async Task<int> CreateAsync(TblClientesDTO cliente)
        {
            return await _repository.CreateAsync(MapToEntity(cliente));
        }

        public async Task<bool> UpdateAsync(TblClientesDTO cliente)
        {
            return await _repository.UpdateAsync(MapToEntity(cliente));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static TblClientesDTO MapToDto(TblClientes tblClientes) =>
            new TblClientesDTO
            {
                Id = tblClientes.Id,
                RazonSocial = tblClientes.RazonSocial,
                IdTipoCliente = tblClientes.IdTipoCliente,
                FechaCreacion = tblClientes.FechaCreacion,
                RFC = tblClientes.RFC
            };

        private static TblClientes MapToEntity(TblClientesDTO tblClientes) =>
            new TblClientes
            {
                Id = tblClientes.Id,
                RazonSocial = tblClientes.RazonSocial,
                IdTipoCliente = tblClientes.IdTipoCliente,
                FechaCreacion = tblClientes.FechaCreacion,
                RFC = tblClientes.RFC
            };
    }
}
