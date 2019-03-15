using System;
using JulioStore.Domain.StoreContext.Enums;
using JulioStore.Shared.Entities;

namespace JulioStore.Domain.StoreContext.Entities
{
    public class Delivery: Entity
    {
        public Delivery(DateTime estimatedDeliveryDate)
        {
            this.CreateDate = DateTime.Now;;
            this.EstimatedDeliveryDate = estimatedDeliveryDate;
            this.Status = eDELIVERYSTATUS.Waiting;
        }
        public DateTime CreateDate { get; private set; }
        public DateTime EstimatedDeliveryDate { get; private set; }
        public eDELIVERYSTATUS Status { get; private set; }

        public void Ship()
        {
            Status = eDELIVERYSTATUS.Shipped;
        }

         public void Cancel()
        {
            Status = eDELIVERYSTATUS.Canceled;
        }
    }
}