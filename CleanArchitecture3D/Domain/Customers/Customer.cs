using Domain.Primitives;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers
{
    public sealed class Customer:AggregateRoot
    {
        public Customer(CustomerID id,string name,string lasName,string email,PhoneNumber phoneNumber,Address)
        {
                
        }
    }
}
