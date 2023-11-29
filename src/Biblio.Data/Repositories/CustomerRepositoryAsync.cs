using Biblio.Domain.Entities;
using Biblio.Domain.Interfaces;
using Biblio.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio.Infrastructure.Data.Repositories
{
    public class CustomerRepositoryAsync: ICustomerRepositoryAsync
    {
        private readonly AppDbContext _context;

        public CustomerRepositoryAsync(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> Create(Customer client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task Update(Customer client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
    }
}
