using System;
using FluentValidator;
using JulioStore.Domain.StoreContext.Enums;
using JulioStore.Shared.Commands;
namespace JulioStore.Domain.StoreContext.Commands.CustomerCommands.Inputs
{
    public class AddAddressComand : Notifiable, ICommand
    {
        public AddAddressComand(Guid id, string street, string number, string complement, string district, string city, string state, string country, string zipCode, eADRESSTYPE type)
        {
            this.Id = id;
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
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public eADRESSTYPE Type { get; set; }

        public event EventHandler CanExecuteChanged;

        bool ICommand.Valid()
        {
            return Valid;
        }
    }
}