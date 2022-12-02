// PascalCase
// camelCase

Console.WriteLine("=============[ Exercicio 2 ]================\n");

Console.WriteLine("Digite a quantidade de litros disponíveis no posto");
double quantidadeLitros = Convert.ToDouble(Console.ReadLine());

Console.WriteLine("Digite o valor do litro de combustível");
double valorLitro = Convert.ToDouble(Console.ReadLine());

Console.WriteLine("=============[ Litros do posto cadastrado com sucesso ]================");
Console.WriteLine($"Total de litros: {quantidadeLitros}");
Console.WriteLine($"Valor total de combustível: R$ {quantidadeLitros * valorLitro}");
Console.WriteLine("=======================================================================\n");

Console.WriteLine("Olá seja muito bem vindo, digite o seu nome: ");
var nomeCliente = Console.ReadLine();

Console.WriteLine($"Olá {nomeCliente}, qual o valor você deseja colocar de combustível?");
var valorQueClienteDeseja = Convert.ToDouble(Console.ReadLine());

var litrosRetiradaCliente = valorQueClienteDeseja / valorLitro;

quantidadeLitros -= litrosRetiradaCliente;

Console.WriteLine($"{nomeCliente}, Você colocou {litrosRetiradaCliente} litros em seu veículo");

Console.WriteLine("=============[ Sobrou no posto ]================");
Console.WriteLine($"Total de litros: {quantidadeLitros}");
Console.WriteLine($"Valor total de combustível: R$ {quantidadeLitros * valorLitro}");
Console.WriteLine("=======================================================================\n");


