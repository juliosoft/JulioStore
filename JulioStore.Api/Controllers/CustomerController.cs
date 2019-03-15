using System;
using System.Collections.Generic;
using JulioStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using JulioStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using JulioStore.Domain.StoreContext.Entities;
using JulioStore.Domain.StoreContext.Handlers;
using JulioStore.Domain.StoreContext.Queries;
using JulioStore.Domain.StoreContext.Repositories;
using JulioStore.Domain.StoreContext.ValueObjects;
using JulioStore.Shared.Commands;
using Microsoft.AspNetCore.Mvc;
namespace JulioStore.Api.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerHandler _customerHandler;
        public CustomerController(ICustomerRepository customerRepository, CustomerHandler customerHandler)
        {
            _customerRepository = customerRepository;
            _customerHandler = customerHandler;
        }

        [HttpGet]
        //[ResponseCache(Duration = 5)]
        [Route("v1/customers/")]
        public IEnumerable<ListCustomerQueryResult> Get()
        {         
            return _customerRepository.GetList();
        }

        [HttpGet]
        [Route("v1/customers/{id}")]
        public GetCustomerQueryResult GetById(Guid id)
        {
            return _customerRepository.GetById(id);
        }

        [HttpGet]
        [Route("v1/customers/{id}/orders")]
        public IEnumerable<ListCustomerOrdersQueryResult> GetOrders(Guid id)
        {
            return _customerRepository.GetOrders(id);
        }

        [HttpPost]
        [Route("v1/customers")]
        public ICommandResults Post([FromBody]CreateCustomerCommand command)
        {
            var  result = (CreateCustomerCommandResult)_customerHandler.Handle(command);
            return result;
        }

        [HttpPut]
        [Route("v1/customers/{id}")]
        public Customer Put([FromBody]CreateCustomerCommand command)
        {
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email =  new Email(command.Email);
            var customer = new Customer(name,document, email, command.Phone);

            return customer;
        }

        [HttpDelete]
        [Route("v1/customers/{id}")]
        public object Delete(Guid id)
        {
            return new {message = "OK"};
        }
    }
}