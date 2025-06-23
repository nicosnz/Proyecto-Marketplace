using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace marketplace_backend.dtos
{
    public class ProductoImagen
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; } = default!;
        public int ProductoId { get; set; } = default!;
        public string FileName { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public byte[] Data { get; set; } = default!;
        public List<Comentariodto> Comentarios { get; set; } = new List<Comentariodto>();

    }
}