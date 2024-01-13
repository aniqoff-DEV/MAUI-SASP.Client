using SASP.Client.Dtos;
using SASP.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASP.Client.DataServices
{
    public interface IOrderRestDataService
    {
        Task<List<OrderDto>> GetAllAsync();
        Task<OrderDto> GetAsync(int idItem);
        Task<Order> GetRowAsync(int idItem);
        Task AddAsync(Order item);
        Task UpdateAsync(Order item);
        Task DeleteAsync(int id);
    }
}
