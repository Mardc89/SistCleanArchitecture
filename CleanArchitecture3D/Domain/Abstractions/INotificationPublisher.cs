using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface INotificationPublishers
    {
        Task PublishAsync(INotifications notification,CancellationToken cancellationToken=default);
    }

    public class NotificationPublishers : INotificationPublishers
    {
        private readonly IServiceProvider _provider;

        public NotificationPublishers(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task PublishAsync(INotifications notification, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(INotificationHandlers<>).MakeGenericType(notification.GetType());

            var handlers = (IEnumerable<object>)_provider.GetService(
                typeof(IEnumerable<>).MakeGenericType(handlerType)
            ) ?? Enumerable.Empty<object>();

            foreach (var handler in handlers)
            {
                var method = handlerType.GetMethod("HandleAsync")!;

                await (Task)method.Invoke(handler, new object[] { notification, cancellationToken })!;
            }
        }
    }

}
