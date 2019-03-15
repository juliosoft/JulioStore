using FluentValidator;
using FluentValidator.Validation;
using JulioStore.Shared.Commands;

namespace JulioStore.Domain.StoreContext.Commands.CustomerCommands.Inputs
{
    public class CreateCustomerCommand : Notifiable, ICommand
    {
        // fail fast validdation
        public string FirstName { get; set; }       
        public string LastName { get; set; }       
        public string Document { get; set; }       
        public string Email { get; set; }       
        public string Phone { get; set; }

        public bool Valid()
        {
            AddNotifications(
                new ValidationContract()
                .Requires()
                .HasMinLen(FirstName, 3, "FirstName", "O nome deve conter no mínimo 3 caracteres")
                .HasMaxLen(FirstName, 30, "FirstName", "O nome deve conter no mácimo 30 caracteres")
                .HasMinLen(LastName, 3, "LastName", "O sobrenome deve conter no mínimo 3 caracteres")
                .HasMaxLen(LastName, 30, "LastName", "O sobrenome deve conter no mácimo 30 caracteres")
                .IsEmail(Email,"Email", "O email é inválido")
                .HasLen(Document, 11, "Document", "CPF Inválido")
            );   

            return !Invalid;
        }
    }
}