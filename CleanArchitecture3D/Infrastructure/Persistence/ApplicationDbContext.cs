using Application.Data;
using Domain.Abstractions;
using Domain.Customers;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
    {
        public INotificationPublisher _publisher;
        public DbSet<Customer> Customers { get; set; }


    }
}
