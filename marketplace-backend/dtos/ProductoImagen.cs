using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace marketplace_backend.dtos
{
    public class ProductoImagen
    {

        [BsonId]
        public string Id { get; set; }
        public int ProductoId { get; set; } 
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }

    }
}