using JulioStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using JulioStore.Domain.StoreContext.Entities;
using JulioStore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JulioStore.Tests.Commands
{
    [TestClass]
    public class CreateCustomerCommandTests
    {
        [TestMethod]
        public void ShouldValidateWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "Julio";
            command.LastName = "Bicharra";
            command.Document = "81721080287";
            command.Email = "jcmail.amm@gmail.com";
            command.Phone = "987110052";

            Assert.AreEqual(true, command.Valid());
        }
    }
}


