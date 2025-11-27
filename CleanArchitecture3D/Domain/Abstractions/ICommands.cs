using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface ICommands<TResult>
    {
    }

    public readonly struct Units
    {
        public static readonly Units Value = new Units();
    }

    public interface ICommandHandlers<TCommands,TResult>
        where TCommands : ICommands<TResult>
    {
        Task<TResult> HandleAsync(TCommands command,CancellationToken cancellationToken);
    }
}
