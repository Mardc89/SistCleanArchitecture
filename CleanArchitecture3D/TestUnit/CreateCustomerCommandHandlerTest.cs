using Application.Customers.Create;
using Domain.Customers;
using Domain.Primitives;
using Moq;

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
        public void HandleCreateCustomer_WhenPhoneNumberHasBadFormat_ShouldReturnValidationError()
        {

        }
    }
}