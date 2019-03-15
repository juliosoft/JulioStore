
using System.Collections.Generic;
using JulioStore.Domain.StoreContext.ValueObjects;
using System.Linq;
using FluentValidator;
using JulioStore.Shared.Entities;

namespace JulioStore.Domain.StoreContext.Entities
{
using System.Collections.Generic;
    public class Customer : Entity
    {
        private readonly IList<Address> _addresses;
        public Customer(Name name, Document document, Email email, string phone)
        {
            this.Name = name;
            this.Document = document;
            this.Email = email;
            this.Phone = phone;
            _addresses = new List<Address>();
        }
        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public string Phone { get; private set; }
        public IReadOnlyCollection<Address> Addresses => _addresses.ToArray();

        public override string ToString() => $"{Name.ToString()}";

        public void AddAddress(Address address)
        {
            _addresses.Add(address);
        }
    }
}