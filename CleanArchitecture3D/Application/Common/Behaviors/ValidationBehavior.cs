using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Behavior
{
    public class ValidationBehavior<TRequest,TResponse>: IPipeLineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest>? _validator;
        public ValidationBehavior(IValidator<TRequest>? validator)
        {
          _validator = validator;
        }
    }
}
