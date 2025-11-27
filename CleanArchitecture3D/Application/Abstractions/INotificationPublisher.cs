using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface INotificationPublisher
    {
        //Task PublishAsync<TNotification>(TNotification notification) where TNotification : INotification;
        Task PublishAsync(INotification notification, CancellationToken cancellationToken = default);
    }

    public class NotificationPublisher : INotificationPublisher
    {
        private readonly IServiceProvider _provider;

        public NotificationPublisher(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task PublishAsync(INotification notification, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(INotificationHandler<>).MakeGenericType(notification.GetType());

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
