using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            return services; 
        }
    }
}
