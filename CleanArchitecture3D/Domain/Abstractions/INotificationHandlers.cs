using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface INotificationHandlers<TNotification> where TNotification : INotifications
    {
        Task HandleAsync(TNotification notification,CancellationToken cancellationToken=default);
    }
}
