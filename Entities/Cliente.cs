using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string UsuarioId { get; set; } = string.Empty;

        public string TipoId { get; set; } = string.Empty;

        public string Identificacion { get; set; } = string.Empty;

        public string NombreCompleto { get; set; } = string.Empty;

        public string GeneroId { get; set; } = string.Empty;

        public string Direccion { get; set; } = string.Empty;

        public string TelCel { get; set; } = string.Empty;

        public string EstadoId { get; set; } = string.Empty;
    }
}
