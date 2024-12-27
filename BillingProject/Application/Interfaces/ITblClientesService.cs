using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITblClientesService
    {
        Task<IEnumerable<TblClientesDTO>> GetAllAsync();
        Task<int> CreateAsync(TblClientesDTO cliente);
        Task<bool> UpdateAsync(TblClientesDTO cliente);
        Task<bool> DeleteAsync(int id);
    }
}
