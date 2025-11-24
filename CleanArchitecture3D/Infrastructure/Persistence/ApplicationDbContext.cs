using Application.Data;
using Domain.Abstractions;
using Domain.Customers;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
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

        public ApplicationDbContext(DbContextOptions options,INotificationPublisher publisher):base(options)
        {
                _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var domainEvents = ChangeTracker.Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEvents().Any())
                .SelectMany(e => e.GetDomainEvents());

           var result=await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents) { 
            
                await _publisher.PublishAsync(domainEvent,cancellationToken);
            
            }


            return result;
        }
    }
}
