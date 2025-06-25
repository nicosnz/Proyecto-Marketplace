using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace marketplace_backend.dtos
{
    public class ProductoImagenConComentarios
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; } = default!;
        public int ProductoId { get; set; } = default!;
        public string NombreArchivo{ get; set; } = default!;
        public string TipoContenido { get; set; } = default!;
        public byte[] Imagen { get; set; } = default!;
        public List<ComentarioDto> Comentarios { get; set; } = new List<ComentarioDto>();

    }
}