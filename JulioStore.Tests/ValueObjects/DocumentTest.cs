using JulioStore.Domain.StoreContext.Entities;
using JulioStore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JulioStore.tests
{
    [TestClass]
    public class Documenttest
    {
        private Document validDocument;
        private Document invalidDocument;
        public Documenttest()
        {
            validDocument = new Document("00975012207");
            invalidDocument = new Document("123456778910");
        }
        [TestMethod]
        public void DeveRetornarFalseQuandoInvalido()
        {
            Assert.AreEqual(true, invalidDocument.Invalid);
        }

        [TestMethod]
        public void DeveRetornarTrueQuandoValido()
        {
            Assert.AreEqual(false, validDocument.Invalid);
        }
    }
}
