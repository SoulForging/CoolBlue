using PointOfSale.Interfaces;
using System;
using DataContracts.Models;

namespace PointOfSale.Controllers
{
    public class EmailMessaging : IMessaging
    {
        public bool SendMessage(Customer customer, string message)
        {
            throw new NotImplementedException();
        }
    }
}
