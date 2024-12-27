using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICatTipoClienteService
    {
        Task<IEnumerable<CatTipoClienteDTO>> GetAllAsync();
        Task<int> CreateAsync(CatTipoClienteDTO tipoCliente);
        Task<bool> UpdateAsync(CatTipoClienteDTO tipoCliente);
        Task<bool> DeleteAsync(int id);
    }
}
