using Logica.Models;

namespace Logica.Servicos;

class ContaCorrenteServico
{
    private ContaCorrenteServico() { }

    private static ContaCorrenteServico instancia = default!;

    public static ContaCorrenteServico Get(){
        if(instancia == null) instancia = new ContaCorrenteServico();
        return instancia;
    }

    public List<ContaCorrente> Lista = new List<ContaCorrente>();

    public List<ContaCorrente> ExtratoCliente(string idCliente)
    {
        var contaCorreteCliente = this.Lista.FindAll(cc => cc.IdCliente == idCliente);
        if(contaCorreteCliente.Count == 0) return new List<ContaCorrente>();

        return contaCorreteCliente;
    }

    public double SaldoCliente(string idCliente, List<ContaCorrente>? contaCorreteCliente = null)
    {
        if(contaCorreteCliente == null)
            contaCorreteCliente = ExtratoCliente(idCliente);

        if(contaCorreteCliente.Count == 0) return 0;

        return contaCorreteCliente.Sum(cc => cc.Valor);
    }
}