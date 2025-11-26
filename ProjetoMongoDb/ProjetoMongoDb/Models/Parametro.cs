using MongoDB.Bson.Serialization.Attributes;

namespace ProtocoloRural.Models
{
    public class Parametro
    {
        [BsonElement("texto")]
        public string Texto { get; set; } = string.Empty;

        [BsonElement("valor")]
        public int Valor { get; set; }
    }
}