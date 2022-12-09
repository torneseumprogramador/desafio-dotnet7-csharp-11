using Programa.Infra.Interfaces;
using MongoDB.Bson;

namespace Programa.Test.infa.Entidades;

public record ClienteMongoDb : ICollectionMongoDb
{
    public ObjectId Id { get;set; }
    public string Nome { get; set; } = default!;
    public string Telefone { get;set; } = default!;
    public string Email { get;set; } = default!;
}