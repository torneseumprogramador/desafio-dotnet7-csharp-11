using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programa.Models;
using Programa.Servicos;

namespace Programa.Test.Models;

[TestClass]
public class ClienteServicoTest
{
    [TestMethod]
    public void TestandoUnicaInstanciaDoServico()
    {
        Assert.IsNotNull(ClienteServico.Get());
        Assert.IsNotNull(ClienteServico.Get().Lista);

        ClienteServico.Get().Lista.Add(new Cliente(){
            Nome = "teste"
        });

        Assert.AreEqual(1, ClienteServico.Get().Lista.Count);
    }
}