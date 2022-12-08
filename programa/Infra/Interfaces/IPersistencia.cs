namespace Programa.Infra.Interfaces;
public interface IPersistencia<T>
{
    Task Salvar(T objeto);
    Task ExcluirTudo();
    Task Excluir(T objeto);
    Task<List<T>> Todos();
    Task<T> BuscaPorId(string id);

    string GetLocalGravacao();
}