using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainErrors
{
    public static partial class Errors
    {
        public static class Customer
        {
            public static Error PhoneNumberWidthBadFormat =>
                Error.Validation("customer.PhoneNumber","Phone number has not valid format.");

            public static Error AddressNumberWidthBadFormat =>
                Error.Validation("customer.Adress", "Adress is not valid");
        }

    }
}
