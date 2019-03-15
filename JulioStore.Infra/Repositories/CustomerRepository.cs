using JulioStore.Domain.StoreContext.Entities;
using JulioStore.Domain.StoreContext.Repositories;
using JulioStore.Infra.StoreContext.DataContext;
using Dapper;
using System.Data;
using System.Linq;
using JulioStore.Domain.StoreContext.Queries;
using System.Collections.Generic;
using System;

namespace JulioStore.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly JulioStoreContext _context;  
        public CustomerRepository(JulioStoreContext context)
        {
            _context = context;
        }
        public bool CheckDocument(string document)
        {
            return _context
                    .Connection.Query<bool>(
                        "spCheckDocument", 
                        new { Document = document},
                        commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();            
        }

        public bool CheckEmail(string email)
        {
            return _context
                    .Connection.Query<bool>(
                        "spCheckEmail", 
                        new { Email = email},
                        commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();            
        }

        public GetCustomerQueryResult GetById(Guid id)
        {
            return _context
                    .Connection.Query<GetCustomerQueryResult>(
                        "Select [ID], CONCAT([FIRSTNAME], ' ', [LASTNAME]) as NAME, [DOCUMENT], [EMAIL] FROM [CUSTOMER] WHERE ID = @ID" 
                        ,new {ID = id}).FirstOrDefault();
        }

        public CustomerOrdersCountResult GetCustomerOrdersCount(string document)
        {
            return _context
                    .Connection.Query<CustomerOrdersCountResult>(
                        "spGetCustomerOrdersCount", 
                        new { Document = document},
                        commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();  
        }

        public IEnumerable<ListCustomerQueryResult> GetList()
        {
            return _context
                    .Connection.Query<ListCustomerQueryResult>(
                        "Select [ID], CONCAT([FIRSTNAME], ' ', [LASTNAME]) AS NAME, [DOCUMENT], [EMAIL] FROM [CUSTOMER]" 
                        ,new {});
        }

        public IEnumerable<ListCustomerOrdersQueryResult> GetOrders(Guid id)
        {
           return _context
                    .Connection.Query<ListCustomerOrdersQueryResult>(
                        "spGetCustomerOrdersCount", 
                        new { id = id},
                        commandType: CommandType.StoredProcedure);
        }

        public void Save(Customer customer)
        {
            _context.Connection.Execute(
            "spCreateCustomer", 
            new 
            { 
                Id = customer.Id,
                Firsname = customer.Name.FirstName,
                LastName = customer.Name.LastName,
                Document = customer.Document.Number,
                Email = customer.Email.Address,
                Phone = customer.Phone,
            },
            commandType: CommandType.StoredProcedure);
        

            foreach (var address in customer.Addresses)
            {
                _context.Connection.Execute("spCreateCustomer", 
                new 
                { 
                    Id = address.Id,
                    CustomerId = customer.Id,
                    Number = address.Number,
                    Complement = address.Complement,
                    District = address.District,
                    City = address.City,
                    State = address.State,
                    Country = address.Country,
                    ZipCode = address.ZipCode,
                    Type = address.Type
                },
                commandType: CommandType.StoredProcedure);
            }

        }
    }
}