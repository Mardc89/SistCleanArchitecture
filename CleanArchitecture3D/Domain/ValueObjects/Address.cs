using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public partial record Address
    { 
       
        public Address(string country,string linea1,string linea2,string city,string state,string zipcode)
        {
            Country=country;               
        }
        public string Country { get; init; }
        public string Linea1 { get; init; }
        public string Linea2 { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string Zipcode { get; init; }

        public static Address()
        {

        }

       
    }
}
