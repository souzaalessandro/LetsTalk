using System;
using LetsTalk.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LetsTalk.Tests
{
    [TestClass]
    public class PerfilControllerTest
    {
        [TestMethod]
        public void DeveRetornarTagsSeparadasVirgulas()
        {
            string tags = "  jogos pizza  lasanha ";

            var splited = tags.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string tagsJoin = String.Join(",", splited);

            string expect = "jogos,pizza,lasanha";

            Assert.AreEqual(expect, tagsJoin);
        }
    }
}
