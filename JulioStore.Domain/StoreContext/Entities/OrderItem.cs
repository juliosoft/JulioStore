using FluentValidator;
using JulioStore.Shared.Entities;

namespace JulioStore.Domain.StoreContext.Entities
{
    public class OrderItem : Entity
    {
        public OrderItem(Product product, decimal quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
            Price = product.Price;

            if(product.QuantityOnHand < quantity)
            {
                AddNotification("Quantity", "Produto fora de estoque.");
            }

            product.DecreaseQuantity(quantity);
        }
        public Product Product { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal Price { get; private set; }
    }
}