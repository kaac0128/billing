using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICatTipoClienteRepository
    {
        Task<IEnumerable<CatTipoCliente>> GetAllAsync();
        Task<int> CreateAsync(CatTipoCliente producto);
        Task<bool> UpdateAsync(CatTipoCliente producto);
        Task<bool> DeleteAsync(int id);
    }
}
