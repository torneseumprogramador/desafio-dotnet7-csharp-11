using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programa.Models;

namespace Programa.Test.Models;

[TestClass]
public class ContaCorrenteTest
{
    [TestMethod]
    public void TestandoPropriedadesDaClasse()
    {
        var contaCorrenteTest = new ContaCorrente();
        contaCorrenteTest.IdCliente = "234321";
        contaCorrenteTest.Valor = 1;
        contaCorrenteTest.Data = DateTime.Now;

        Assert.AreEqual("234321", contaCorrenteTest.IdCliente);
        Assert.AreEqual(1, contaCorrenteTest.Valor);
        Assert.IsNotNull(contaCorrenteTest.Data);
    }
}