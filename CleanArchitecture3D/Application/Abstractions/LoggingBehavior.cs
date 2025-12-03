using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public class LoggingBehavior<TCommand, TResponse>: ICommandPipeLineBehavior<TCommand, TResponse>
    {
        public async Task<TResponse> HandleAsync(
            TCommand command,
            CommandHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            Console.WriteLine($"[LOG] Ejecutando comando {typeof(TCommand).Name}");

            var response = await next();

            Console.WriteLine($"[LOG] Comando {typeof(TCommand).Name} finalizado");

            return response;
        }
    
    }
}
