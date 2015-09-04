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
                Price = Price
            };
        }

        public CartItem(SalesDealContainer discountedDeal)
        {
            this.cartDiscountDeal = discountedDeal;
            cartOrderLine = new OrderLine()
            {
                ProductID = cartDiscountDeal.SubProduct.ProductID,
                Quantity = 1,
                Price = Price
            };
        }

        public decimal Price
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
            cartOrderLine.Price = cartOrderLine.Quantity * Price;
        }

        public string CartSingleHeading
        {
            get
            {
                if (cartProduct != null)
                    return string.Format("{0}x {1}", cartOrderLine.Quantity, cartProduct.Name);
                else
                    return string.Format("{0}x {1} AND {2} Combo", cartOrderLine.Quantity, cartDiscountDeal.MainProduct.Name, cartDiscountDeal.SubProduct.Name);
            }
        }
    }
}
