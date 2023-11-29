using Biblio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> CreateClient(RequestCreateCustomerDTO client);
        Task<Customer> GetClientById(int id);
        Task<IEnumerable<Customer>> GetAllClients();
        Task UpdateClient(Customer client);
        Task DeleteClient(int id);
    }
}
