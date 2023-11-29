using Biblio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio.Domain.Interfaces
{
    public interface ICustomerRepositoryAsync
    {
        Task<Customer> Create(Customer client);
        Task<Customer> GetById(int id);
        Task<IEnumerable<Customer>> GetAll();
        Task Update(Customer client);
        Task Delete(int id);
    }
}
