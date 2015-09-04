using DataContracts.Models;

namespace PointOfSale.DataStructures
{
    public class CartItem
    {
        public Product cartProduct { get; set; }
        public SalesDealContainer cartDiscountDeal { get; set; }
        public OrderLine cartOrderLine { get; set; }


        public CartItem(Product product)
        {
            this.cartProduct = product;
            cartOrderLine = new OrderLine()
            {
                ProductID = product.ProductID,
                Quantity = 1,
                Price = price
            };
        }

        public CartItem(SalesDealContainer discountedDeal)
        {
            this.cartDiscountDeal = discountedDeal;
            cartOrderLine = new OrderLine()
            {
                ProductID = cartDiscountDeal.SubProduct.ProductID,
                Quantity = 1,
                Price = price
            };
        }

        private decimal price
        {
            get
            {
                if (this.cartProduct != null)
                    return this.cartProduct.Price;

                return this.cartDiscountDeal.DiscountedPrice;
            }
        }

        public void AddOne()
        {
            cartOrderLine.Quantity++;
            cartOrderLine.Price = cartOrderLine.Quantity * price;
        }


    }
}
