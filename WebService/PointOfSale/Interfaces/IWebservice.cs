using DataContracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointOfSale.Interfaces
{
    public interface IWebservice
    {
        Task<Product> GetProduct(int ID);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> SearchProducts(string criteria);

        Task<IEnumerable<SalesCombination>> GetSalesCombinations(int ID);

        Task<bool> UpdateCustomer(Customer toUpdate);
        Task<Customer> AddCustomer(Customer toAdd);
        Task<Customer> SearchForCustomer(string searchString);
    }
}
