using FluentValidator;
using JulioStore.Shared.Entities;

namespace JulioStore.Domain.StoreContext.Entities
{
    public class Product: Entity
    {

        public Product(string title, string description, string image, decimal price, decimal quantityOnHand)
        {
            this.Title = title;
            this.Description = description;
            this.Image = image;
            this.Price = price;
            this.QuantityOnHand = quantityOnHand;

        }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public decimal Price { get; private set; }
        public decimal QuantityOnHand { get; private set; }

        public override string ToString() => this.Description;

        public void DecreaseQuantity(decimal quantity)
        {
            QuantityOnHand -= quantity;
        }
    }
}