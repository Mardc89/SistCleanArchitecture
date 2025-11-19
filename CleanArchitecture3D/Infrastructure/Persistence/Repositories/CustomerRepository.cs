using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
              _context = context?? throw new ArgumentNullException(nameof(context)); 
        }

        public async Task Add(Customer customer)=>await _context.Customers.AddAsync(customer);
        public async Task<Customer?> GetByIdAsync(CustomerID id)=>await _context.Customers.SingleOrDefaultAsync(x => x.Id == id);

    }
}
