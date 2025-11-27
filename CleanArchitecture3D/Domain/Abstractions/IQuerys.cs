using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{

    public interface IQuerys<TResult>
    {
    }

    public interface IQueryHandler<TQuery,TResult>
        where TQuery : IQuerys<TResult>
    {
        Task<TResult> HandleAsync(TQuery command);
    }
}
