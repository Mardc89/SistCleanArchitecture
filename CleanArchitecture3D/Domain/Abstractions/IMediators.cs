using ErrorOr;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Domain.Abstractions
{
    public interface IMediators
    {
        Task SendAsync<TCommand,TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommands<TResult>;
        Task<TResult> SendAsync<TQuery,TResult>(TQuery query) where TQuery:IQuerys<TResult>;
    }

    public class Mediator : IMediators
    {
        private readonly IServiceProvider _provider;
        public Mediator(IServiceProvider provider)
        {
            _provider = provider;
        }
        public async Task SendAsync<TCommand,TResult>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommands<TResult>
        {
            var handler=_provider.GetRequiredService<ICommandHandlers<TCommand,TResult>>();
            await handler.HandleAsync(command,cancellationToken);
        }

        public async Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuerys<TResult>
        {
            var handler = _provider.GetRequiredService<IQueryHandler<TQuery,TResult>>();
            return await handler.HandleAsync(query);
        }
    }
}
