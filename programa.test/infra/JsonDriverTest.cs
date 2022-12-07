using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programa.Models;

namespace Programa.Infra;

[TestClass]
public class JsonDriverTest
{
    public JsonDriverTest()
    {
        var caminho = Environment.GetEnvironmentVariable("LOCAL_GRAVACAO_TEST_DESAFIO_DOTNET7") ?? "/tmp";
        this.caminhoArquivoTest = caminho;
        this.jsonDriver = new JsonDriver(this.caminhoArquivoTest);
    }

    private string caminhoArquivoTest;
    private JsonDriver jsonDriver;

    [TestMethod]
    public async Task TestandoDriverJsonParaClientes()
    {
       
        var cliente = new Cliente(){
            Id = Guid.NewGuid().ToString(),
            Nome = "Danilo",
            Email = "danilo@teste.com",
            Telefone = "(11)9999-9999"
        };

        await this.jsonDriver.Salvar(cliente);

        var existe = File.Exists(this.caminhoArquivoTest + "/clientes.json");
    }

    [TestMethod]
    public async Task TestandoDriverJsonParaContaCorrente()
    {
        var contaCorrente = new ContaCorrente(){
            IdCliente = Guid.NewGuid().ToString(),
            Valor = 200,
            Data = DateTime.Now
        };

        await jsonDriver.Salvar(contaCorrente);

        var existe = File.Exists(this.caminhoArquivoTest + "/clientes.json");
    }
}