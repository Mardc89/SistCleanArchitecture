using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface ISenders
    {
        Task<TResponse> SendAsync<TResponse>(ICommands<TResponse> command,CancellationToken cancellationToken=default);
        Task<TResult> SendAsync<TResult>(IQuerys<TResult> query, CancellationToken cancellationToken = default);
    }
}
