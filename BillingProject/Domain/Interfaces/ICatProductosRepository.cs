using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICatProductosRepository
    {
        Task<IEnumerable<CatProductos>> GetAllAsync();
        Task<int> CreateAsync(CatProductos producto);
        Task<bool> UpdateAsync(CatProductos producto);
        Task<bool> DeleteAsync(int id);
    }
}
