using System;
using System.Collections.Generic;
using JulioStore.Domain.StoreContext.Enums;
using System.Linq;
using FluentValidator;
using JulioStore.Shared.Entities;

namespace JulioStore.Domain.StoreContext.Entities
{
    public class Order : Entity
    {
        private readonly IList<OrderItem> _items;
        private readonly IList<Delivery> _deliveries;

        public Order(Customer costumer)
        {
            this.Costumer = costumer;            
            CreateDate = DateTime.Now;
            Status = eORDERSTATUS.Created;
            _items = new List<OrderItem>();
            _deliveries = new List<Delivery>();
        }

        public Customer Costumer { get; private set; }
        public string Number { get; private set; }
        public DateTime CreateDate { get; private set; }
        public eORDERSTATUS Status { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _items.ToArray();

        public IReadOnlyCollection<Delivery> Deliveries => _deliveries.ToArray();

        public void AddItem(Product product, decimal quantity)
        {
            if(quantity > product.QuantityOnHand)
            {
                AddNotification("OrderItem", $"Produto {product.Title} não tem {quantity} em estoque.");
            }

            var item  = new OrderItem(product, quantity);
            _items.Add(item);
        }
        
        public void Place() 
        { 
           // gerar numero do pedido
           Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0,8).ToUpper();     
           if(_items.Count == 0)
           {
               AddNotification("Order","O pedido não existe item.");
           }
        }

        public void Pay()
        {
            Status = eORDERSTATUS.Paid;            
        }

        public void Ship()
        {
            var deliveries = new List<Delivery>();
            var count = 1;

            // QUebra as entregas
            foreach(var item in _items)
            {
                if(count == 5)
                {
                    count= 0;
                    deliveries.Add(new Delivery(DateTime.Now.AddDays(5)));
                }    
                count++;
            }

            // envia as entregas
            deliveries.ForEach(x=> x.Ship());

            // adiciona as entregas ao pedido
            deliveries.ForEach(x=> _deliveries.Add(x));
        }

        public void Cancel()
        {
            Status = eORDERSTATUS.Canceled;
            _deliveries.ToList().ForEach(x=> x.Cancel());
        }
    }
}