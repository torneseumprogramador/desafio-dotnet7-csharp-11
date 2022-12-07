using System.Text.Json;
using Programa.Infra.Interfaces;

namespace Programa.Infra;

public class JsonDriver : IPersistencia
{
    public JsonDriver(string localGravacao)
    {
        this.localGravacao = localGravacao;
    }

    private string localGravacao = "";
    
    public string GetLocalGravacao()
    {
        return this.localGravacao;
    }

    public async Task Salvar(object objeto)
    {
        string jsonString = JsonSerializer.Serialize(objeto);

        var nome = objeto.GetType().Name.ToLower();
        await File.WriteAllTextAsync($"{this.GetLocalGravacao()}/{nome}s.json", jsonString);
    }

    public void Alterar(string Id, object objeto)
    {
        throw new NotImplementedException();
    }

    public List<object> BuscaPorId(string Id)
    {
        throw new NotImplementedException();
    }

    public void Excluir(object objeto)
    {
        throw new NotImplementedException();
    }

    public List<object> Todos()
    {
        throw new NotImplementedException();
    }
}