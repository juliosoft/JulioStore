using System;
using System.Collections.Generic;
using JulioStore.Domain.StoreContext.Entities;
using JulioStore.Domain.StoreContext.Queries;

namespace JulioStore.Domain.StoreContext.Repositories
{
    public interface ICustomerRepository
    {
         bool CheckDocument(string document);
         bool CheckEmail(string email);
         void Save(Customer customer);

         CustomerOrdersCountResult GetCustomerOrdersCount(string document);

         IEnumerable<ListCustomerQueryResult> GetList();

         GetCustomerQueryResult GetById(Guid id);

         IEnumerable<ListCustomerOrdersQueryResult> GetOrders(Guid id);
    }
}