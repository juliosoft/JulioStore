using System;
using System.Collections.Generic;
using FluentValidator;
using FluentValidator.Validation;
using JulioStore.Shared.Commands;

namespace JulioStore.Domain.StoreContext.Commands.OrderCommands.Input
{
    public class PlaceOrderCommand : Notifiable, ICommand
    {
        public PlaceOrderCommand(Guid costumer) 
        {
            this.Costumer = costumer;
               
        }
        public Guid Costumer { get; set; }   
        public IList<OrderItemCommand> OrderItems { get; set; }

        public bool Valid()
        {
            AddNotifications(
                new ValidationContract()
                .HasLen(Costumer.ToString(), 36, "Costumer", "Identificador inválido")
                .IsGreaterThan(OrderItems.Count, 0, "Items", "Nenhum item do pedido foi encontrado")                
            );   

            return Valid();            
        }
    }

    public class OrderItemCommand
    {
        public Guid Product { get; set; }
        public decimal Quantity { get; set; }
    }
}