List<dynamic> listaDeClientes = new List<dynamic>();
List<dynamic> contaCorrete = new List<dynamic>();

while (true)
{
    Console.Clear();

    Console.WriteLine("""
    =================[Seja bem-vindo à empresa Lina]=================
    O que você deseja fazer?
    1 - Cadastrar o cliente
    2 - Ver extrato cliente
    3 - Crédito em conta
    4 - Retirada
    5 - Sair do sistema
    """);

    var opcao = Console.ReadLine()?.Trim();
    Console.Clear();
    bool sair = false;

    switch (opcao)
    {
        case "1":
            Console.Clear();
            cadastrarCliente();
            break;
        case "2":
            Console.Clear();
            mostrarContaCorrente();
            break;
        case "3":
            Console.Clear();
            adicionarCreditoCliente();
            break;
        case "4":
            Console.Clear();
            fazendoDebitoCliente();
            break;
        case "5":
            sair = true;
            break;
        default:
            Console.WriteLine("Opção inválida");
            break;
    }

    if (sair) break;
}

void mostrarContaCorrente()
{
    Console.Clear();

    if(listaDeClientes.Count == 0 || contaCorrete.Count == 0)
    {
        mensagem("Não existe clientes ou não existe movimentações em conta correte, cadastre o cliente e faça crédito em conta");
        return;
    }

    var cliente = capturaCliente();

    var contaCorrenteCliente = extratoCliente(cliente.Id);
    Console.Clear();
    Console.WriteLine("----------------------");
    foreach(var contaCorrente in contaCorrenteCliente)
    {
        Console.WriteLine("Data: " + contaCorrente.Data.ToString("dd/MM/yyyy HH:mm:ss"));
        Console.WriteLine("Valor: " + contaCorrente.Valor);
        Console.WriteLine("----------------------");
    }
    
    Console.WriteLine($"""
    O valor total da conta do cliente {cliente.Nome} é de:
    R$ {saldoCliente(cliente.Id, contaCorrenteCliente)}


    """);


    Console.WriteLine("Digite enter para continuar");
    Console.ReadLine();
}

void listarClientesCadastrados()
{
    if(listaDeClientes.Count == 0)
    {
        menuCadastraClienteSeNaoExiste();
    }

    mostrarClientes(false, 0, "===============[ Selecione um cliente da lista ]===================");
}

void mostrarClientes(
    bool sleep = true,
    int timerSleep = 2000,
    string header = "===============[ Lista de clientes ]===================")
{
    Console.Clear();
    Console.WriteLine(header);

    foreach(var cliente in listaDeClientes)
    {
        Console.WriteLine("Id:" + cliente.Id);
        Console.WriteLine("Nome:" + cliente.Nome);
        Console.WriteLine("Telefone:" + cliente.Telefone);
        Console.WriteLine("Email:" + cliente.Email);
        Console.WriteLine("----------------------------");

        if(sleep)
        {
            Thread.Sleep(timerSleep);
            Console.Clear();
        }
    }
}

void cadastrarCliente()
{
    var id = Guid.NewGuid().ToString();

    Console.WriteLine("Informe o nome do cliente:");
    var nomeCliente = Console.ReadLine();

    Console.WriteLine($"Informe o telefone do cliente {nomeCliente}: ");
    var telefone = Console.ReadLine();

    Console.WriteLine($"Informe o email do cliente {nomeCliente}: ");
    var email = Console.ReadLine();

    if(listaDeClientes.Count > 0)
    {
        var cli = listaDeClientes.Find(c => c.Telefone == telefone);
        if(cli != null)
        {
            mensagem($"Cliente já cadastrado com este telefone {telefone}, cadastre novamente");
            cadastrarCliente();
        }
    }

    listaDeClientes.Add(new {
        Id = id,
        Nome = nomeCliente ?? "[Sem Nome]",
        Telefone = telefone != null ? telefone : "[Sem Telefone]",
        Email = email ?? "[Sem Email]"
    });
    mensagem($""" {nomeCliente} cadastrado com sucesso. """);    
}

void mensagem(string msg)
{
    Console.Clear();
    Console.WriteLine(msg);
    Thread.Sleep(1500);
}

void fazendoDebitoCliente(){
    Console.Clear();
    var cliente = capturaCliente();
    Console.Clear();
    Console.WriteLine("Digite o valor de retirada:");
    double credito = Convert.ToDouble(Console.ReadLine());

    contaCorrete.Add(new {
        IdCliente = cliente.Id,
        Valor = credito * -1,
        Data = DateTime.Now
    });

    mensagem($"""
    Retirada realizada com sucesso ...
    Saldo do cliente {cliente.Nome} é de R$ {saldoCliente(cliente.Id)}
    """);
}


void adicionarCreditoCliente()
{
    Console.Clear();
    var cliente = capturaCliente();
    Console.Clear();
    Console.WriteLine("Digite o valor do crédito:");
    double credito = Convert.ToDouble(Console.ReadLine());

    contaCorrete.Add(new {
        IdCliente = cliente.Id,
        Valor = credito,
        Data = DateTime.Now
    });

    mensagem($"""
    Credito adicionado com sucesso ...
    Saldo do cliente {cliente.Nome} é de R$ {saldoCliente(cliente.Id)}
    """);
}


List<dynamic> extratoCliente(string idCliente)
{
    var contaCorreteCliente = contaCorrete.FindAll(cc => cc.IdCliente == idCliente);
    if(contaCorreteCliente.Count == 0) return new List<dynamic>();

    return contaCorreteCliente;
}

double saldoCliente(string idCliente, List<dynamic>? contaCorreteCliente = null)
{
    if(contaCorreteCliente == null)
        contaCorreteCliente = extratoCliente(idCliente);

    if(contaCorreteCliente.Count == 0) return 0;

    double soma = 0;
    foreach(var cc in contaCorreteCliente)
    {
        soma += cc.Valor;
    }
    return soma;

    // return contaCorreteCliente.Sum(cc => cc.Valor);
}

dynamic capturaCliente()
{
    listarClientesCadastrados();
    Console.WriteLine("Digite o ID do cliente");
    var idCliente = Console.ReadLine()?.Trim();
    var cliente = listaDeClientes.Find(c => c.Id == idCliente);

    if(cliente == null)
    {
        mensagem("Cliente não encontrado na lista, digite o ID corretamente da lista de clientes");
        Console.Clear();
        
        menuCadastraClienteSeNaoExiste();

        return capturaCliente();
    }

    return cliente;
}

void menuCadastraClienteSeNaoExiste()
{
    Console.WriteLine("""
    O que você deseja fazer ?
    1 - Cadastrar cliente
    2 - Voltar ao menu
    3 - Sair do programa
    """);

    var opcao = Console.ReadLine()?.Trim();

    switch(opcao)
    {
        case "1":
            cadastrarCliente();
            break;
        case "3":
            break;
        case "2":
            System.Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Opção inválida");
            break;
    }
}