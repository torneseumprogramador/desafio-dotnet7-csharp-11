using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programa.Infra;
using Programa.Models;
using Programa.Servicos;

namespace Programa.Test.Models;

[TestClass]
public class ClienteServicoTest
{
    [TestMethod]
    public void TestandoInjecaoDeDependencia()
    {
        var clienteServicoJson = new ClienteServico(new JsonDriver<Cliente>("bla"));
        Assert.IsNotNull(clienteServicoJson);
        Assert.IsNotNull(clienteServicoJson.Persistencia);

        var clienteServicoCsv = new ClienteServico(new CsvDriver<Cliente>("bla"));
        Assert.IsNotNull(clienteServicoCsv);
        Assert.IsNotNull(clienteServicoCsv.Persistencia);
    }
}