using Microsoft.Extensions.DependencyInjection;
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

        //public async Task<TResponse> Send<TResponse>(ICommand<TResponse> command,CancellationToken cancellationToken = default)
        //{
        //    var handler = _serviceProvider.GetRequiredService<ICommandHandler<ICommand<TResponse>, TResponse>>();

        //    // Obtener los pipelines
        //    var behaviors = _serviceProvider
        //        .GetServices<IPipeLineBehavior<,>()
        //        .ToList();

        //    // Construir el pipeline
        //    RequestHandlerDelegate<TResponse> next = () =>
        //        handler.HandleAsync(command, cancellationToken);

        //    foreach (var behavior in behaviors.AsEnumerable().Reverse())
        //    {
        //        var currentNext = next;

        //        next = () => behavior.Handle(
        //            (dynamic)command,
        //            currentNext,
        //            cancellationToken);
        //    }

        //    return await next();
        //}


        //public async Task<TResponse> Send<TResponse>(ICommand<TResponse> command,CancellationToken cancellationToken = default)
        //{
        //    var handlerType = typeof(ICommandHandler<,>)
        //        .MakeGenericType(command.GetType(), typeof(TResponse));

        //    dynamic handler = _serviceProvider.GetRequiredService(handlerType);

        //    var behaviors = _serviceProvider
        //        .GetServices(typeof(ICommandPipeLineBehavior<,>)
        //            .MakeGenericType(command.GetType(), typeof(TResponse)))
        //        .Cast<dynamic>()
        //        .ToList();

        //    CommandHandlerDelegate<TResponse> next = () =>
        //        handler.HandleAsync((dynamic)command, cancellationToken);

        //    foreach (var behavior in behaviors.AsEnumerable().Reverse())
        //    {
        //        var localNext = next;
        //        next = () => behavior.HandleAsync((dynamic)command, localNext, cancellationToken);
        //    }

        //    return await next();
        //}


        //public async Task<TResponse> Send<TResponse>(ICommand<TResponse> command,CancellationToken cancellationToken = default)
        //{
        //    // Obtener el handler específico
        //    var handlerType = typeof(ICommandHandler<,>)
        //        .MakeGenericType(command.GetType(), typeof(TResponse));

        //    dynamic handler = _serviceProvider.GetRequiredService(handlerType);

        //    // Obtener behaviors específicos del comando concreto
        //    var behaviorType = typeof(IPipeLineBehavior<,>)
        //        .MakeGenericType(command.GetType(), typeof(TResponse));

        //    var behaviors = _serviceProvider
        //        .GetServices(behaviorType)
        //        .Cast<dynamic>()
        //        .Reverse()
        //        .ToList();

        //    // Construir pipeline: último → el handler
        //    Func<Task<TResponse>> pipeline = () => handler.HandleAsync((dynamic)command, cancellationToken);

        //    // Cada behavior envuelve al siguiente
        //    foreach (dynamic behavior in behaviors)
        //    {
        //        var next = pipeline;

        //        pipeline = () => behavior.Handle(
        //            (dynamic)command,
        //            cancellationToken,
        //            next
        //        );
        //    }

        //    return await pipeline();
        //}


        public async Task<TResponse> Send<TResponse>(ICommand<TResponse> request,CancellationToken cancellationToken = default)
        {
            // Resolver el handler principal
            var handlerType = typeof(ICommandHandler<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            dynamic handler = _serviceProvider.GetRequiredService(handlerType);

            // Resolver behaviors
            var behaviorType = typeof(IPipeLineBehavior<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            var behaviors = _serviceProvider
                .GetServices(behaviorType)
                .Cast<dynamic>()
                .ToList();

            // Construimos delegate final (handler real)
            RequestHandlerDelegate<TResponse> next = () =>
                handler.HandleAsync((dynamic)request, cancellationToken);

            // "envolver" el handler en behaviors (como MediatR)
            foreach (var behavior in behaviors.AsEnumerable().Reverse())
            {
                var innerNext = next;

                next = () => behavior.HandleAsync((dynamic)request, innerNext, cancellationToken);
            }

            return await next();
        }

    }
}
