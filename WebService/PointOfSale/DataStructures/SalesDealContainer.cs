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
                return string.Format("Combo deal! Buy both '{0}' and '{1}' with a ${2} discount!", MainProduct.Name, SubProduct.Name, SalesCombination.Discount);
            }
        }

        public decimal DiscountedPrice
        {
            get
            {
                return MainProduct.Price + SubProduct.Price - SalesCombination.Discount;
            }
        }
    }
}
