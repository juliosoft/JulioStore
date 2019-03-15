using JulioStore.Domain.StoreContext.Enums;
using JulioStore.Shared.Entities;

namespace JulioStore.Domain.StoreContext.Entities
{
    public class Address : Entity
    {
          public Address(string street, string number, string complement, string district, string city, string state, string country, string zipCode, eADRESSTYPE type)
        {
            this.Street = street;
            this.Number = number;
            this.Complement = complement;
            this.District = district;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.ZipCode = zipCode;
            this.Type = type;

        }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string District { get; private set; }
      
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        public eADRESSTYPE Type { get; private set; }

        public override string ToString() => $"{Street}, {Number}, {City}/{State}";
    }
}