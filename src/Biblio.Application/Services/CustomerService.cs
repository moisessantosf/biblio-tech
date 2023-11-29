using Biblio.Application.Interfaces;
using Biblio.Domain.Entities;
using Biblio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio.Application.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepositoryAsync _clientRepository;

        public CustomerService(ICustomerRepositoryAsync clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Customer> CreateClient(RequestCreateCustomerDTO customer)
        {
            var newCustomer = new Customer
            {
               Name = customer.Name,
               Email = customer.Email,
            };

            return await _clientRepository.Create(newCustomer);
        }

        public async Task<Customer> GetClientById(int id)
        {
            return await _clientRepository.GetById(id);
        }

        public async Task<IEnumerable<Customer>> GetAllClients()
        {
            return await _clientRepository.GetAll();
        }

        public async Task UpdateClient(Customer client)
        {
            await _clientRepository.Update(client);
        }

        public async Task DeleteClient(int id)
        {
            await _clientRepository.Delete(id);
        }
    }
}
