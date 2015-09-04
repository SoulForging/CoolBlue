using DataContracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointOfSale.Interfaces
{
    public interface IWebservice
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> SearchProducts(string criteria);
    }
}
