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
    

        [Test]
        public void DeveAcertarNomeMaiuscula()
        {
            string formatado = Utilities.FormatarNome("FULANO");

            Assert.AreEqual("Fulano", formatado);
        }

        [Test]
        public void DeveAcertarNomeMinuscula()
        {
            string formatado = Utilities.FormatarNome("fulano");

            Assert.AreEqual("Fulano", formatado);
        }

        [Test]
        public void DeveAcertarNomeCompostoMaiuscula()
        {
            string formatado = Utilities.FormatarNome("FULANO DOS SANTOS");

            Assert.AreEqual("Fulano Dos Santos", formatado);
        }

        [Test]
        public void DeveAcertarNomeCompostoMinuscula()
        {
            string formatado = Utilities.FormatarNome("FULANO DOS SANTOS");

            Assert.AreEqual("Fulano Dos Santos", formatado);
        }
    }
}
