using System;

namespace DataContracts.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string EmailAddress { get; set; }
        public string Street { get; set; }
        public string Housenumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public DateTime EntryDate { get; set; }
    }
}