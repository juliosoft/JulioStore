using System;
using FluentValidator;
using JulioStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using JulioStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using JulioStore.Domain.StoreContext.Entities;
using JulioStore.Domain.StoreContext.Repositories;
using JulioStore.Domain.StoreContext.Services;
using JulioStore.Domain.StoreContext.ValueObjects;
using JulioStore.Shared.Commands;

namespace JulioStore.Domain.StoreContext.Handlers
{
    public class CustomerHandler : Notifiable, ICommandHandler<CreateCustomerCommand>,ICommandHandler<AddAddressComand>        
    {
        private readonly ICustomerRepository _repository;
        private readonly IEmailService _emailService;
        public CustomerHandler(ICustomerRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResults Handle(CreateCustomerCommand command)
        {
            // verificar se cpf ja existe
            if(_repository.CheckDocument(command.Document))
            {
                AddNotification("Document", "Esse CPF já está em uso.");
                return null;
            }

            // verificar se email ja exisite
            if(_repository.CheckEmail(command.Email))
            {
                AddNotification("Email", "Esse Email já está em uso.");
                return null;
            }

            // criar Vo
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);

            // criar entidades
            var customer = new Customer(name,document, email, command.Phone);

            // validar entidades e vo
            AddNotifications(name.Notifications);
            AddNotifications(document.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(name.Notifications);
            AddNotifications(customer.Notifications);

            if(Invalid)
            {
                return new CommandResult(false, "Por favor corriga os campos abaixo.", Notifications);
            }

            // persistir
            _repository.Save(customer);

            // enviar email
            _emailService.Send(email.Address, "jcmail.am@gmail.com", "Bem  Vindo", "Seja Bem Vindo ao JulioSTORE");

            // retornar o resultado para tela
            return new CommandResult(true, "Bem vindo ao Júlio Store", new { Id = customer.Id, Name = customer.Name, Email = customer.Email});
        }

        public ICommandResults Handle(AddAddressComand command)
        {
            throw new System.NotImplementedException();
        }
    }
}