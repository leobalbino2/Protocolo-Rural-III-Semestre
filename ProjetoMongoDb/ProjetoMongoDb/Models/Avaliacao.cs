using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProtocoloRural.Models
{
    [BsonIgnoreExtraElements]
    public class Avaliacao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("usuario_id")]
        public string UsuarioId { get; set; }

        [BsonElement("nome_propriedade")]
        public string NomePropriedade { get; set; } = string.Empty;

        [BsonElement("data_avaliacao")]
        public DateTime DataAvaliacao { get; set; } = DateTime.UtcNow;

        [BsonElement("grau_sustentabilidade")]
        public int GrauSustentabilidade { get; set; }

        [BsonElement("grau_porcentagem")]
        public double GrauPorcentagem { get; set; }

        [BsonElement("finalizado")]
        public bool Finalizado { get; set; } = false;

        [BsonElement("respostas")]
        public List<RespostaAvaliacao> Respostas { get; set; } = new List<RespostaAvaliacao>();
    }

    public class RespostaAvaliacao
    {
        [BsonElement("indicador_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IndicadorId { get; set; } = string.Empty;

        [BsonElement("parametro_id")]
        public int ParametroId { get; set; }

        [BsonElement("valor")]
        public int Valor { get; set; }

        [BsonElement("texto")]
        public string? Texto { get; set; }
    }
}