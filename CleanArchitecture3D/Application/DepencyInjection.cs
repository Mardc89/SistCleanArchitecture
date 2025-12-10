using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Abstractions;
using Application.Customers.Create;
using Domain.Primitives;
using ErrorOr;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Behavior;

namespace Application
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {

                     
            services.AddScoped<INotificationPublishers, NotificationPublishers>();
            services.AddScoped<INotificationHandlers<DomainEvent>, DomainEventHandler>();
    

            services.AddScoped<ISender, Sender>();
            //services.AddValidatorsFromAssemblyContaining<CreateCustomerComandValidator>();
            // Registrar todos tus handlers
            services.AddScoped<ICommandHandler<CreateCustomerCommand, ErrorOr<Unit>>, CreateCustomerCommandHandler>();
            //services.AddScoped(typeof(IPipeLineBehavior<,>), typeof(ValidationBehavior<,>));
            //services.AddTransient(typeof(ICommandPipeLineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipeLineBehavior<,>), typeof(ValidationBehavior<,>));        
            //services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

            //services.AddTransient<IValidator<CreateCustomerCommand>, CreateCustomerComandValidator>();

            return services; 
        }
    }
}
