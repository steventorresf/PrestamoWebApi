using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Entities
{
    public class Tabla
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public List<TablaDetalle> Detalle { get; set; } = new();
        
    }

    public class TablaDetalle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
