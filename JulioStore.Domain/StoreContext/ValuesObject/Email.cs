using FluentValidator;
using FluentValidator.Validation;

namespace JulioStore.Domain.StoreContext.ValueObjects
{
    public class Email : Notifiable
    {
        public Email(string address)
        {
            this.Address = address;
            AddNotifications(
                new ValidationContract()
                .Requires()
                .IsEmail(address,"Email", "O email é inválido")
            );
        }
        public string Address { get; private set; }

        public override string ToString() => Address;
    }
}