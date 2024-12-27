using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICatProductosService
    {
        Task<IEnumerable<CatProductosDTO>> GetAllAsync();
        Task<int> CreateAsync(CatProductosDTO producto);
        Task<bool> UpdateAsync(CatProductosDTO producto);
        Task<bool> DeleteAsync(int id);
    }
}
