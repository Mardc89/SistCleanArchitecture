using Application.Abstractions;
using ErrorOr;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Behavior
{
    public class ValidationBehavior<TRequest,TResponse>
        : IPipeLineBehavior<TRequest, TResponse> 
        where TRequest:IRequest<TResponse> 
        where TResponse:IErrorOr
       
    {
        private readonly IValidator<TRequest>? _validator;
        public ValidationBehavior(IValidator<TRequest>? validator=null)
        {
          _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator is null) { 
                return await next();
            }

            var validatorResult=await _validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.IsValid)
            {
                return await next();

            }

            var errors = validatorResult.Errors
                .ConvertAll(validatonFailure => Error.Validation(
                    validatonFailure.PropertyName,
                    validatonFailure.ErrorMessage
                    ));

            return (dynamic)errors;
        }
    }
}
