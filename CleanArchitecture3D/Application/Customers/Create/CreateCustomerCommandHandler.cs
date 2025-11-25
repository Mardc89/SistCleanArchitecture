using Application.Abstractions;
using Domain.Customers;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Create
{
    internal sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand,ErrorOr<Unit>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitWork;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository,IUnitOfWork unitOfWorks)
        {

            _customerRepository = customerRepository ?? throw new ArgumentNullException((nameof(customerRepository))) ;
            _unitWork = unitOfWorks ?? throw new ArgumentNullException(nameof(unitOfWorks));
            
        }
        public async Task<ErrorOr<Unit>> HandleAsync(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
                {
                    return Error.Validation("customer.PhoneNumber", "Phone number has not valid format");
                }

                if (Address.Create(command.City,command.Linea1,command.Linea2,command.Country,command.State,
                    command.ZipCode) is not Address address)
                {
                    return Error.Validation("customer.Adress", "Adress is not valid");
                }

                var customer = new Customer(
                    new CustomerID(Guid.NewGuid()),
                    command.Name,
                    command.LastName,
                    command.Email,
                    phoneNumber,
                    address,
                    true
                    
                 );


                await _customerRepository.Add(customer);
                await _unitWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception ex)
            {

                return Error.Failure("CreateCustomer.Failure",ex.Message);
            }
        }
    }
}
