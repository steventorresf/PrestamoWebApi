using Domain.Response;
using Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace Persistence.Repositories.Implementations
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly MongoDBContext _context;

        public ClienteRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<ResponseListItem<Cliente>> GetClientes(string uid, string? textFilter, int pageNumber, int pageSize)
        {
            FilterDefinition<Cliente> filter = Builders<Cliente>.Filter.Eq(x => x.UsuarioId, uid) & Builders<Cliente>.Filter.Regex(x => x.NombreCompleto, BsonRegularExpression.Create(new Regex(textFilter ?? string.Empty, RegexOptions.IgnoreCase)));
            ResponseListItem<Cliente> response = new()
            {
                CountItems = await _context.ClienteCollection.CountDocumentsAsync(filter).ConfigureAwait(false),
                ListItems = await _context.ClienteCollection.Find(filter)
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync()
                .ConfigureAwait(false)
            };

            //var query = (from cli in _clienteCollection.AsQueryable()
            //             join usu in _usuarioCollection.AsQueryable() on cli.UsuarioId equals usu.Id
            //             where usu.Id == new ObjectId(uid) && cli.NombreCompleto.Contains(textFilter ?? string.Empty)
            //             select cli);

            //ResponseListItem<Cliente> response = new()
            //{
            //    CountItems = await query.CountAsync(),
            //    ListItems = await query
            //    .Skip((pageNumber - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToListAsync()
            //    .ConfigureAwait(false)
            //};

            return response;
        }

        public async Task PostCliente(Cliente cliente)
        {
            await _context.ClienteCollection.InsertOneAsync(cliente).ConfigureAwait(false);
        }

        //public async Task PutCliente(Cliente cliente)
        //{
        //    FilterDefinition<Cliente> filter = Builders<Cliente>.Filter.Eq(x => x.Id, cliente.Id);
        //    var clienteDB = _clienteCollection.FindOneAndUpdate(filter, cliente);
        //    await _clienteCollection.InsertOneAsync(cliente).ConfigureAwait(false);
        //}
    }
}
