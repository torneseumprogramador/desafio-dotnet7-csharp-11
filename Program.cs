﻿Console.WriteLine("PayBank");
//investidor, saldo inicial, ao clocar o saldo descontar 0,05
Console.WriteLine("Seja bem vindo ao PagBank, Qual seu nome por favor?");
var nomeCliente = Console.ReadLine();
Console.WriteLine("Digite o valor que deseja depositar?");
var deposito = Convert.ToDouble(Console.ReadLine());
var valorEmConta = deposito -(deposito * 0.05)/100;

Console.WriteLine($"""
nome: {nomeCliente}
deposito: {deposito}
valor final: {valorEmConta}
""");

Console.WriteLine($"""
Deseja sacar o valor atual da conta? R$ {valorEmConta}
Digite 1 para sim e 2 para não.
""");
var escolha = Convert.ToInt16(Console.ReadLine());

if(escolha ==1){
    Console.Write("Qual valor deseja sacar? ");
    var saque = Convert.ToDouble(Console.ReadLine());

    if(saque <= valorEmConta){
            valorEmConta-= saque;
    Console.WriteLine($"Saque Realizado com Sucesso, saldo atual R$ {valorEmConta}");
    }else{
        Console.WriteLine($"Desculpe Valor superior ao disponivel na conta. Saldo atual R$ {valorEmConta}");
    }
}else{
    Console.WriteLine("Obrigado por utilizar os serviços PayBank");
}