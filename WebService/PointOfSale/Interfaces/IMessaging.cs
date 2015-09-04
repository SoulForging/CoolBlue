using DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Interfaces
{
    public interface IMessaging
    {
        bool SendMessage(Customer customer, string message);
    }
}
