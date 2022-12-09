using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programa.Models;
using Programa.Infra;

namespace Programa.Test.Infra;

[TestClass]
public class EntityMySqlDriverTest
{
    public EntityMySqlDriverTest()
    {
        var caminho = Environment.GetEnvironmentVariable("LOCAL_GRAVACAO_TEST_DESAFIO_DOTNET7_MYSQL") ?? "Server=localhost;Database=dotnet7_driver;Uid=root;Pwd=root;";
        this.caminhoArquivoTest = caminho;
    }

    private string caminhoArquivoTest;

    [TestInitialize()]
    public async Task Startup()
    {
        await new EntityMySqlDriver<Cliente>(this.caminhoArquivoTest).ExcluirTudo();
        await new EntityMySqlDriver<ContaCorrente>(this.caminhoArquivoTest).ExcluirTudo();
    }

    [TestMethod]
    public async Task TestandoDriverJsonParaClientes()
    {
        var jsonDriver = new EntityMySqlDriver<Cliente>(this.caminhoArquivoTest);
       
        var cliente = new Cliente(){
            Id = Guid.NewGuid().ToString(),
            Nome = "Danilo",
            Email = "danilo@teste.com",
            Telefone = "(11)9999-9999"
        };

        await jsonDriver.Salvar(cliente);

        var existe = File.Exists(this.caminhoArquivoTest + "/clientes.json");
    }

    [TestMethod]
    public async Task TestandoDriverJsonParaContaCorrente()
    {
        var jsonDriver = new EntityMySqlDriver<ContaCorrente>(this.caminhoArquivoTest);

        var contaCorrente = new ContaCorrente(){
            Id = Guid.NewGuid().ToString(),
            IdCliente = Guid.NewGuid().ToString(),
            Valor = 200,
            Data = DateTime.Now
        };

        await jsonDriver.Salvar(contaCorrente);

        var existe = File.Exists(this.caminhoArquivoTest + "/contacorrentes.json");
    }

    [TestMethod]
    public async Task TestandoBuscaDeTodasAsEntidades()
    {
        var jsonDriver = new EntityMySqlDriver<ContaCorrente>(this.caminhoArquivoTest);
        
        var contaCorrente = new ContaCorrente(){
            Id = Guid.NewGuid().ToString(),
            IdCliente = Guid.NewGuid().ToString(),
            Valor = 200,
            Data = DateTime.Now
        };

        await jsonDriver.Salvar(contaCorrente);

        var todos = await jsonDriver.Todos();

        Assert.IsTrue(todos.Count > 0);
    }

    [TestMethod]
    public async Task TestandobuscaPorId()
    {
        var jsonDriver = new EntityMySqlDriver<Cliente>(this.caminhoArquivoTest);
        
        var cliente = new Cliente(){
            Id = Guid.NewGuid().ToString(),
            Nome = "Danilo " + DateTime.Now,
            Email = "danilo@teste.com",
            Telefone = "(11)9999-9999"
        };

        await jsonDriver.Salvar(cliente);

        var clienteDb = await jsonDriver.BuscaPorId(cliente.Id);

        Assert.AreEqual(cliente.Nome, clienteDb?.Nome);
    }

    [TestMethod]
    public async Task TestandoAlteracaoDeEntidade()
    {
        var jsonDriver = new EntityMySqlDriver<Cliente>(this.caminhoArquivoTest);
        
        var cliente = new Cliente(){
            Id = Guid.NewGuid().ToString(),
            Nome = "Danilo",
            Email = "danilo@teste.com",
            Telefone = "(11)9999-9999"
        };

        await jsonDriver.Salvar(cliente);

        cliente.Nome = "Danilo Santos";

        await jsonDriver.Salvar(cliente);

        var clienteDb = await jsonDriver.BuscaPorId(cliente.Id);

        Assert.AreEqual("Danilo Santos", clienteDb?.Nome);
    }

    [TestMethod]
    public async Task TestandoExcluirEntidade()
    {
        var jsonDriver = new EntityMySqlDriver<ContaCorrente>(this.caminhoArquivoTest);
        
        var contaCorrente = new ContaCorrente(){
            Id = Guid.NewGuid().ToString(),
            IdCliente = Guid.NewGuid().ToString(),
            Valor = 200,
            Data = DateTime.Now
        };

        await jsonDriver.Salvar(contaCorrente);

        var objDb = await jsonDriver.BuscaPorId(contaCorrente.Id);
        Assert.IsNotNull(objDb);
        Assert.IsNotNull(objDb?.Id);

        await jsonDriver.Excluir(contaCorrente);

        var objDb2 = await jsonDriver.BuscaPorId(contaCorrente.Id);
        Assert.IsNull(objDb2);
    }
}