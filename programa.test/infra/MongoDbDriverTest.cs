using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programa.Models;
using Programa.Infra;
using Programa.Test.infa.Entidades;
using MongoDB.Bson;

namespace Programa.Test.infa;

[TestClass]
public class MongoDbDriverTest
{
    public MongoDbDriverTest()
    {
        var caminho = Environment.GetEnvironmentVariable("LOCAL_GRAVACAO_TEST_DESAFIO_DOTNET7_MYSQL") ?? "mongodb://localhost#desafio21dias_dotnet7";
        this.caminhoArquivoTest = caminho;
    }

    private string caminhoArquivoTest;

    [TestInitialize()]
    public async Task Startup()
    {
        await new MongoDbDriver<ClienteMongoDb>(this.caminhoArquivoTest).ExcluirTudo();
        await new MongoDbDriver<ContaCorrenteMongoDb>(this.caminhoArquivoTest).ExcluirTudo();
    }

    [TestMethod]
    public async Task TestandoDriverJsonParaClientes()
    {
        var jsonDriver = new MongoDbDriver<ClienteMongoDb>(this.caminhoArquivoTest);
       
        var cliente = new ClienteMongoDb(){
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
        var jsonDriver = new MongoDbDriver<ContaCorrenteMongoDb>(this.caminhoArquivoTest);

        var contaCorrente = new ContaCorrenteMongoDb(){
            IdCliente = ObjectId.GenerateNewId(),
            Valor = 200,
            Data = DateTime.Now
        };

        await jsonDriver.Salvar(contaCorrente);

        var existe = File.Exists(this.caminhoArquivoTest + "/contacorrentes.json");
    }

    [TestMethod]
    public async Task TestandoBuscaDeTodasAsEntidades()
    {
        var jsonDriver = new MongoDbDriver<ContaCorrenteMongoDb>(this.caminhoArquivoTest);
        
        var contaCorrente = new ContaCorrenteMongoDb(){
            IdCliente = ObjectId.GenerateNewId(),
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
        var jsonDriver = new MongoDbDriver<ClienteMongoDb>(this.caminhoArquivoTest);
        
        var cliente = new ClienteMongoDb(){
            Nome = "Danilo " + DateTime.Now,
            Email = "danilo@teste.com",
            Telefone = "(11)9999-9999"
        };

        await jsonDriver.Salvar(cliente);

        var clienteDb = await jsonDriver.BuscaPorId(cliente.Id.ToString());

        Assert.AreEqual(cliente.Nome, clienteDb?.Nome);
    }

    [TestMethod]
    public async Task TestandoAlteracaoDeEntidade()
    {
        var jsonDriver = new MongoDbDriver<ClienteMongoDb>(this.caminhoArquivoTest);
        
        var cliente = new ClienteMongoDb(){
            Nome = "Danilo",
            Email = "danilo@teste.com",
            Telefone = "(11)9999-9999"
        };

        await jsonDriver.Salvar(cliente);

        cliente.Nome = "Danilo Santos";

        await jsonDriver.Salvar(cliente);

        var clienteDb = await jsonDriver.BuscaPorId(cliente.Id.ToString());

        Assert.AreEqual("Danilo Santos", clienteDb?.Nome);
    }

    [TestMethod]
    public async Task TestandoExcluirEntidade()
    {
        var jsonDriver = new MongoDbDriver<ContaCorrenteMongoDb>(this.caminhoArquivoTest);
        
        var contaCorrente = new ContaCorrenteMongoDb(){
            IdCliente = ObjectId.GenerateNewId(),
            Valor = 200,
            Data = DateTime.Now
        };

        await jsonDriver.Salvar(contaCorrente);

        var objDb = await jsonDriver.BuscaPorId(contaCorrente.Id.ToString());
        Assert.IsNotNull(objDb);
        Assert.IsNotNull(objDb?.Id);

        await jsonDriver.Excluir(contaCorrente);

        var objDb2 = await jsonDriver.BuscaPorId(contaCorrente.Id.ToString());
        Assert.IsNull(objDb2);
    }
}