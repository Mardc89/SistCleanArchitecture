using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions;
using Domain.Primitives;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddSingleton<INotificationPublisher, NotificationPublisher>();
            services.AddTransient<INotificationHandler<DomainEvent>, DomainEventHandler>();
            services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();
            return services; 
        }
    }
}
