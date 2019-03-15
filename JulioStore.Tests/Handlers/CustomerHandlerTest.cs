using JulioStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using JulioStore.Domain.StoreContext.Handlers;
using JulioStore.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JulioStore.Tests.Handlers
{
    [TestClass]
    public class CustomerHandlerTest
    {
        [TestMethod]
        public void ShouldRegisterCustomerWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "Julio";
            command.LastName = "Bicharra";
            command.Document = "81721080287";
            command.Email = "jcmail.amm@gmail.com";
            command.Phone = "987110052";


            var handler = new CustomerHandler(new FakeCustomerRepository(), new FakeEmailService());
            var result = handler.Handle(command);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(true, handler.Valid);
        }
    }
}