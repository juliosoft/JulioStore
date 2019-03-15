using JulioStore.Domain.StoreContext.Entities;
using JulioStore.Domain.StoreContext.Enums;
using JulioStore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JulioStore.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private Customer _costumer;
        private Order _order;
        private Product _mouse;
        private Product _monitor;
        private Product _keyboard;
        public OrderTests()
        {
            var  name = new Name("Júlio", "Cesar");
            var document = new Document("81721080287");
            var email =  new Email("jcmail.am@gmail.com");
            _costumer = new Customer(name,document, email, "19 987110052");
            _order = new Order(_costumer);
            _mouse = new Product("Mouse Optico", "Mouse de Fio Optico", "image.png", 100M, 10);
            _monitor = new Product("Monitor 22' ", "Monitor 22 polegadas", "image.png", 100M, 10);
            _keyboard = new Product("Teclado", "Teclado PTBR", "image.png", 100M, 10);
        }
        // Consigo criar um novo pedido
        [TestMethod]
        public void ShouldCreateOrderWhenValid()
        {
            
            Assert.AreEqual(false, _order.Invalid);
        }

        // Ao criar o pedido, o status deverá ser created
        [TestMethod]
        public void StatusShouldBeCreatedWhenOrderCreated()
        {

            Assert.AreEqual(eORDERSTATUS.Created, _order.Status);
        } 

        // Ao adicionar um novo item, a quantidade de item deve mudar
        [TestMethod]
        public void ShouldReturnTwoWhenAddedTwoValidItems()
        {
            _order.AddItem(_monitor, 5);
            _order.AddItem(_mouse, 5);            
            Assert.AreEqual(2, _order.Items.Count);
        } 

        // Ao adicionar um novo item, deve subtrair a quantidade do produto
        [TestMethod]
        public void ShouldReturnFiveWhenAddedPurchasedFiveItem()
        {
            _order.AddItem(_monitor, 5);
            Assert.AreEqual(5, _monitor.QuantityOnHand);
        } 

        // Ao confirmar pedido, deve gerar um numero
        [TestMethod]
        public void ShouldReturnNumberWhenOrderPlaced()
        {
            _order.Place();
            Assert.AreNotEqual("", _order.Number);
        } 

        // Ao pagar um pedido, o status deve ser PAGO
        [TestMethod]
        public void ShouldReturnPaidWhenOrderPlaced()
        {
            _order.Pay();
            Assert.AreEqual(eORDERSTATUS.Paid, _order.Status);
        }

        // Dados mais 10 produtos, deve haver duas entregas
        [TestMethod]
        public void ShoudlReturnTwoWhenPurchasedThenProducts()
        {
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            
            _order.Ship();
            Assert.AreEqual(2, _order.Deliveries.Count);
        }

        // Ao cancelar pedido, o status deve ser cancelado
        [TestMethod]
        public void StatusShouldBeCanceledWhenOrderCanceled()
        {
            _order.Cancel();
            Assert.AreEqual(eORDERSTATUS.Canceled, _order.Status);
        }

        // Ao cancelar pedido, deve cancelar as entregas
        [TestMethod]
        public void ShouldBeCancelShippingsWhenOrderCanceled()
        {
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            
            _order.Ship();
            _order.Cancel();

            foreach(var item in _order.Deliveries)
            {
                Assert.AreEqual(eDELIVERYSTATUS.Canceled, item.Status);
            }
        }
    }
}