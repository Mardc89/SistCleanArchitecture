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
            Linea1=linea1;
            Linea2=linea2;
            City=city;
            State=state;
            Zipcode=zipcode;
        }
        public string Country { get; init; }
        public string Linea1 { get; init; }
        public string Linea2 { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string Zipcode { get; init; }

        public static Address? Create(string country, string linea1, string linea2, string city, string state, string zipcode)
        {
            if (string.IsNullOrEmpty(country) || string.IsNullOrEmpty(linea1) ||
                string.IsNullOrEmpty(linea2)  || string.IsNullOrEmpty(city) ||
                string.IsNullOrEmpty(state)   || string.IsNullOrEmpty(zipcode))
            { return null; }
            
            return new Address(country, linea1, linea2, city, state, zipcode);

        }

       
    }
}
