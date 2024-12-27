using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITblClientesRepository
    {
        Task<IEnumerable<TblClientes>> GetAllAsync();
        Task<int> CreateAsync(TblClientes producto);
        Task<bool> UpdateAsync(TblClientes producto);
        Task<bool> DeleteAsync(int id);
    }
}
