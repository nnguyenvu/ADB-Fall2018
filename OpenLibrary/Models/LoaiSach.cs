using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Models
{
    public class LoaiSach
    {
        
        public Int32 _id { get; set; }
        [BsonElement("ten_loai_sach")]
        public String TenLoaiSach { get; set; }
        [BsonElement("id_loai_cha")]
        public Int32 IdLoaiCha { get; set; }
        [BsonElement("trang_thai")]
        public Int32 TrangThai { get; set; }

        
    }
}
