using MongoDB.Bson;

namespace Programa.Infra.Interfaces;
public interface ICollectionMongoDb
{
    ObjectId Id { get;set; }
}