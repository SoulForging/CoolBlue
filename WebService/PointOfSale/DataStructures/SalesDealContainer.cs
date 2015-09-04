using DataContracts.Models;

namespace PointOfSale.DataStructures
{
    public class SalesDealContainer
    {
        public SalesCombination SalesCombination { get; set; }
        public Product MainProduct { get; set; }
        public Product SubProduct { get; set; }

        public SalesDealContainer(SalesCombination salesCombination, Product mainProduct, Product subProduct)
        {
            this.SalesCombination = salesCombination;
            this.MainProduct = mainProduct;
            this.SubProduct = subProduct;
        }

        public string ComboDealText
        {
            get
            {
                return string.Format("Combo deal! Add the following item with your existing '{0}' at a ${1} discount!", MainProduct.Name, SalesCombination.Discount);
            }
        }

        public decimal DiscountedPrice
        {
            get
            {
                return SubProduct.Price - SalesCombination.Discount;
            }
        }
    }
}
