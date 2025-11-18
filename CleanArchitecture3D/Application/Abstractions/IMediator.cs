using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Abstractions
{
    public interface IMediator
    {
        Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand;
        Task<TResult> SendAsync<TQuery,TResult>(TQuery query) where TQuery:IQuery<TResult>;
    }

    public class Mediator : IMediator
    {
        private readonly IServiceProvider _provider;
        public Mediator(IServiceProvider provider)
        {
            _provider = provider;
        }
        public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
        {
            var handler=_provider.GetRequiredService<ICommandHandler<TCommand>>();
            await handler.HandleAsync(command,cancellationToken);
        }

        public async Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = _provider.GetRequiredService<IQueryHandler<TQuery,TResult>>();
            return await handler.HandleAsync(query);
        }
    }
}
