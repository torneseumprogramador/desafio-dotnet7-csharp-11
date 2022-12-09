using System.ComponentModel;
using System.Text.Json;
using Programa.Infra.Interfaces;

namespace Programa.Infra;

public class AdoMySqlDriver<T> : IPersistencia<T>
{
    public AdoMySqlDriver(string localGravacao)
    {
        this.localGravacao = localGravacao;
    }

    private string localGravacao = "";
    
    public string GetLocalGravacao()
    {
        return this.localGravacao;
    }

    public async Task Salvar(T objeto)
    {
        throw new NotImplementedException();
    }

    public async Task<T?> BuscaPorId(string id)
    {
        throw new NotImplementedException();
    }

    public async Task Excluir(T objeto)
    {
        throw new NotImplementedException();
    }

    public async Task<List<T>> Todos()
    {
        throw new NotImplementedException();
    }

    public async Task ExcluirTudo()
    {
        throw new NotImplementedException();
    }
}