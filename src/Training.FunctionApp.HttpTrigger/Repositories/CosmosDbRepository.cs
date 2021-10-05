using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Authentication;

namespace Training.FunctionApp.HttpTrigger.Repositories
{
    public class CosmosDbRepository
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<UserMongo> _mongoCollection;

        public CosmosDbRepository()
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(Environment.GetEnvironmentVariable("CosmosDB_ConnectionString")));

            settings.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            settings.RetryWrites = false;

            _mongoClient = new MongoClient(settings);

            _mongoDatabase = _mongoClient.GetDatabase(Environment.GetEnvironmentVariable("CosmosDB_Database"));

            _mongoCollection = _mongoDatabase.GetCollection<UserMongo>(Environment.GetEnvironmentVariable("CosmosDB_Collection"));
        }

        public void Save(UserMongo user)
        {
            _mongoCollection.InsertOne(user);
        }

        public ICollection<UserMongo> GetAll()
        {
            return _mongoCollection.Find(_ => true).ToList();
        }
    }
}
