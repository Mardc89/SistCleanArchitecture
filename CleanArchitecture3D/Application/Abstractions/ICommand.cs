using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface ICommand<TResult>
    {
    }

    public readonly struct Unit
    {
        public static readonly Unit Value = new Unit();
    }

    public interface ICommandHandler<TCommand,TResult>where TCommand : ICommand<TResult>
    {
        Task<TResult> HandleAsync(TCommand command,CancellationToken cancellationToken);
    }
}
