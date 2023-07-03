using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities
{
    public class Usuario
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public List<Cliente> Clientes { get; set; } = new();
    }
}
