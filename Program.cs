List<string[]> lista = new List<string[]>();

while (true)
{
    Console.Clear();

    Console.WriteLine("""
    =================[Seja bem-vindo à empresa Lina]=================
    O que você deseja fazer?
    1 - Cadastrar o cliente
    2 - Ver conta corrente
    3 - Fazer crédito em conta
    4 - Fazer débito em conta
    5 - Sair do sistema
    6 - Mostrar
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

            break;
        case "3":
            Console.Clear();

            break;
        case "4":
            Console.Clear();

            break;
        case "5":
            sair = true;
            break;
        
        case "6":
            mostrarClientes();
            break;
        default:
            Console.WriteLine("Opção inválida");
            break;
    }

    if (sair) break;
    Thread.Sleep(4000);
}

void mostrarClientes()
{
    Console.Clear();
    foreach(var cliente in lista)
    {
        Console.WriteLine("Nome:" + cliente[0]);
        Console.WriteLine("Telefone:" + cliente[1]);
        Console.WriteLine("Email:" + cliente[2]);

        Thread.Sleep(2000);
        Console.Clear();
    }
}

void cadastrarCliente()
{
    var id = Guid.NewGuid();

    Console.WriteLine("Informe o nome do cliente:");
    var nomeCliente = Console.ReadLine();

    Console.WriteLine($"Informe o telefone do cliente {nomeCliente}: ");
    var telefone = Console.ReadLine();

    Console.WriteLine($"Informe o email do cliente {nomeCliente}: ");
    var email = Console.ReadLine();

    string[] cliente = new string[4];

    cliente[0] = id.ToString();
    cliente[1] = nomeCliente != null ? nomeCliente : "[Sem Nome]";
    cliente[2] = telefone != null ? telefone : "[Sem Telefone]";
    cliente[3] = email != null ? email : "[Sem Email]";

    lista.Add(cliente);
    mensagem($""" {nomeCliente} cadastrado com sucesso. """);    
}

void mensagem(string msg)
{
    Console.Clear();
    Console.WriteLine(msg);
    Thread.Sleep(1500);
}