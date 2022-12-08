using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using Programa.Infra.Interfaces;

namespace Programa.Infra;

public class JsonDriver<T> : IPersistencia<T>
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

    public async Task Salvar(T objeto)
    {
        if(objeto == null) return;

        var lista = await Todos();
        var id = objeto.GetType().GetProperty("Id")?.GetValue(objeto)?.ToString();
        
        if(string.IsNullOrEmpty(id)) return;

        var objLista = buscaListaId(lista, id);
        if(objLista == null || objLista.GetType().GetProperty("Id")?.GetValue(objLista)?.ToString() == null) 
            lista.Add(objeto);
        else atualizaPropriedades(objeto, objLista);

        await salvarLista(lista);
    }

    private async Task salvarLista(List<T> lista)
    {
        string jsonString = JsonSerializer.Serialize(lista);
        var nome = typeof(T).Name.ToLower();
        await File.WriteAllTextAsync($"{this.GetLocalGravacao()}/{nome}s.json", jsonString);
    }

    private void atualizaPropriedades(T objetoDe, T objListaPara)
    {
        if(objetoDe == null || objListaPara == null) return;

        foreach(var propDe in objetoDe.GetType().GetProperties())
        {
            var propPara = objListaPara.GetType().GetProperty(propDe.Name);
            if(propPara != null)
            {
                var valorDe = propDe.GetValue(objetoDe);
                propPara.SetValue(objListaPara, valorDe);
            }
        }
    }

    public async Task<List<T>> Todos()
    {
        var nome = typeof(T).Name.ToLower();

        var arquivo = $"{this.GetLocalGravacao()}/{nome}s.json";

        if(!File.Exists(arquivo)) return new List<T>();

        string jsonString = await File.ReadAllTextAsync(arquivo);
        var lista = JsonSerializer.Deserialize<List<T>>(jsonString);
        return lista ?? new List<T>();
    }

    public async Task<T?> BuscaPorId(string id)
    {
        var lista = await Todos();
        return buscaListaId(lista, id);
    }
    private T? buscaListaId(List<T> lista, string id)
    {
        return lista.Find(o => o?.GetType().GetProperty("Id")?.GetValue(o)?.ToString() == id);
    }

    // ANTES
    // private T buscaListaId(List<T> lista, string id)
    // {
    //     return lista.Find(o => o.GetType().GetProperty("Id")?.GetValue(o)?.ToString() == id);
    // }

    public async Task Excluir(T objeto)
    {
        var lista = await Todos();
        lista.Remove(objeto);
        await salvarLista(lista);
    }

    public async Task ExcluirTudo()
    {
        var nome = typeof(T).Name;
        await File.WriteAllTextAsync($"{this.GetLocalGravacao()}/{nome}s.json", "[]");
    }
}