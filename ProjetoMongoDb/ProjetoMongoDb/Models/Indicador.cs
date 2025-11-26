using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProtocoloRural.Models
{
    [BsonIgnoreExtraElements]
    public class Indicador
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "O nome do indicador é obrigatório.")]
        [BsonElement("nome")]
        public string Nome { get; set; } = string.Empty;

        [BsonElement("descricao")]
        public string Descricao { get; set; } = string.Empty;

        [BsonElement("estado")]
        public bool Estado { get; set; } = false;

        [BsonElement("criado_em")]
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        [BsonElement("atualizado_em")]
        public DateTime? AtualizadoEm { get; set; }

        [BsonElement("parametros")]
        public List<Parametro> Parametros { get; set; } = new List<Parametro>();
    }
}