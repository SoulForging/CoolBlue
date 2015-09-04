using DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Controllers
{
    public class CustomersController : ApiController
    {
        Customer[] products = new Customer[]
        {
            new Customer() { CustomerID=1, FirstName="Gin", LastName="Bottle", EmailAddress="GinTheAlcoholic@gmail.com", PostalCode="1778", City="Johannesburg", Housenumber="37", MiddleName="NA", Street="Shackles" },
            new Customer() { CustomerID=2, FirstName="Jack", LastName="Daniels", EmailAddress="JackTheChamp@gmail.com", PostalCode="2001", City="Houston", Housenumber="1337", MiddleName="Om-Nom", Street="Homeless" },
        };

        public Customer Add(Customer toAdd)
        {
            return null;
        }

        public bool Update(Customer toUpdate)
        {
            return false;
        }
    }
}
