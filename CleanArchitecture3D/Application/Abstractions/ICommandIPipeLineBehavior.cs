using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public delegate Task<TResponse> CommandHandlerDelegate<TResponse>();
    public interface ICommandIPipeLineBehavior<TCommand,TResponse>
    {
        Task<TResponse> HandleAsync(
            TCommand command,
            CommandHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken);
    }
}
