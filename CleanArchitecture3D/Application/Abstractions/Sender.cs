using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Abstractions
{
    public class Sender:ISender
    {
        private readonly IServiceProvider _serviceProvider;

        public Sender(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(ICommandHandler<,>)
                            .MakeGenericType(command.GetType(), typeof(TResponse));
            dynamic? handler = _serviceProvider.GetService(handlerType);

            if (handler is null)
            {
                throw new InvalidOperationException($"No handler found for {command.GetType().Name}");
            }

            return await handler.HandleAsync((dynamic)command, cancellationToken);
        }

        public async Task<TResult> SendAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(query.GetType(), typeof(TResult));

            dynamic? handler = _serviceProvider.GetService(handlerType);

            if (handler is null)
            {
                throw new InvalidOperationException($"No handler found for {query.GetType().Name}");
            }

            return await handler.HandleAsync((dynamic)query, cancellationToken);
        }
    }
}
