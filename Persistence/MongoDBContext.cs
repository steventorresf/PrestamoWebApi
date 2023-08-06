using Domain.DTO;
using Entities;
using MongoDB.Driver;

namespace Persistence
{
    public class MongoDBContext
    {
        private readonly MongoCollections _mongoCollections;
        private readonly MongoClient _context;
        private readonly IMongoDatabase _database;

        public MongoDBContext(MongoSettings mongoSettings)
        {
            _mongoCollections = mongoSettings.MongoCollections;
            _context = new MongoClient(mongoSettings.ServerName);
            _database = _context.GetDatabase(mongoSettings.Database);
        }

        public IMongoCollection<Cliente> ClienteCollection
        {
            get { return _database.GetCollection<Cliente>(_mongoCollections.Cliente); }
        }

        public IMongoCollection<Tabla> TablaCollection
        {
            get { return _database.GetCollection<Tabla>(_mongoCollections.Tabla); }
        }

        public IMongoCollection<Usuario> UsuarioCollection
        {
            get { return _database.GetCollection<Usuario>(_mongoCollections.Usuario); }
        }
    }
}
