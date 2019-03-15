using System;

namespace JulioStore.Domain.StoreContext.Queries
{
    public class CustomerOrdersCountResult
    {
        public Guid Id { get; set; }        
        public string Name { get; set; }        
        public int Orders { get; set; }        

        public string   Document { get; set; }
    }
}