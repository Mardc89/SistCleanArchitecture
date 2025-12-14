using Application.Customers.Create;
using Domain.Customers;
using Domain.Primitives;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Domain.DomainErrors;
namespace TestUnit
{
    public class CreateCustomerCommandHandlerTest
    {
        private readonly Mock<ICustomerRepository> _mockcustomerRepository;

        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly CreateCustomerCommandHandler _handler;

        public CreateCustomerCommandHandlerTest()
        {
            _mockcustomerRepository = new Mock<ICustomerRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new CreateCustomerCommandHandler(_mockcustomerRepository.Object, _mockUnitOfWork.Object);

                
        }
        [Fact]
        public async Task HandleCreateCustomer_WhenPhoneNumberHasBadFormat_ShouldReturnValidationError()
        {
          CreateCustomerCommand command=new CreateCustomerCommand(
              "Lobert",
              "Lopez",
              "lopez34gmail.com",
              "3232434434",
              "",
              "",
              "",
              "",
              "",
              ""
              
              
          );

            var result = await _handler.HandleAsync(command, default);

            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorOr.ErrorType.Validation);
            result.FirstError.Code.Should().Be(Errors.Customer.PhoneNumberWidthBadFormat.Code);
            result.FirstError.Description.Should().Be(Errors.Customer.PhoneNumberWidthBadFormat.Description);
        }
    }
}