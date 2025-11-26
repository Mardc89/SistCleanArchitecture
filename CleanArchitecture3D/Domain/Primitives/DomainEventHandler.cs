using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Primitives
{
    public class DomainEventHandler : INotificationHand<DomainEvent>
    {
        public Task HandleAsync(DomainEvent notification, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Evento recibido:{notification.Id }");
            return Task.CompletedTask;
        }
    }
}
