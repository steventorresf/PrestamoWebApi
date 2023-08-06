using Entities;
using MongoDB.Driver;

namespace Persistence.Repositories.Implementations
{
    public class TablaRepository : ITablaRepository
    {
        private readonly MongoDBContext _context;

        public TablaRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<Tabla> GetTablaByCodigo(string codigo)
        {
            FilterDefinition<Tabla> filter = Builders<Tabla>.Filter.Eq(x => x.Codigo, codigo);
            return await _context.TablaCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
