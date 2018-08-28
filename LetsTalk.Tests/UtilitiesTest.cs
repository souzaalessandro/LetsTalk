using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using NUnit.Framework;

namespace LetsTalk.Tests
{
    [TestFixture]
    public class UtilitiesTest
    {
        private Utilities _util;

        [SetUp]
        public void SetUp()
        {
            _util = new Utilities();
        }

        [Test]
        public void DeveAcertarNomeMaiuscula()
        {
            string formatado =  _util.FormatarNome("FULANO");

            Assert.AreEqual("Fulano", formatado);
        }

        [Test]
        public void DeveAcertarNomeMinuscula()
        {
            string formatado = _util.FormatarNome("fulano");

            Assert.AreEqual("Fulano", formatado);
        }

        [Test]
        public void DeveAcertarNomeCompostoMaiuscula()
        {
            string formatado = _util.FormatarNome("FULANO DOS SANTOS");

            Assert.AreEqual("Fulano Dos Santos", formatado);
        }

        [Test]
        public void DeveAcertarNomeCompostoMinuscula()
        {
            string formatado = _util.FormatarNome("FULANO DOS SANTOS");

            Assert.AreEqual("Fulano Dos Santos", formatado);
        }
    }
}
