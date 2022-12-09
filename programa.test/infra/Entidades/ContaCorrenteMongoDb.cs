using MongoDB.Bson;
using Programa.Infra.Interfaces;

namespace Programa.Test.infa.Entidades;

public record ContaCorrenteMongoDb : ICollectionMongoDb
{
    public ObjectId Id { get; set; }
    public required ObjectId IdCliente { get; set; }
    public double Valor { get; set; } = default!;
    public DateTime Data { get; set; } = default!;
}