using DataContracts.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using System.Collections.Generic;

namespace PointOfSale.DataStructures
{
    public class NavigateInfo
    {
        public string ScreenName { get; set; }
        public Customer CurrentCustomer { get; set; }
        public IEnumerable<CartItem> CurrentCart { get; set; }
    }

    public class NavigateScreenEvent : PubSubEvent<NavigateInfo>
    {

    }
}
