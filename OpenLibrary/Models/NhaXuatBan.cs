using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Models
{
    public class NhaXuatBan
    {
        public int _id { get; set; }
        [BsonElement("ten_nha_xuat_ban")]
        public string TenNhaXuatBan { get; set; }
        [BsonElement("dia_chi")]
        public string DiaChi { get; set; }
        [BsonElement("dien_thoai")]
        public string DienThoai { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
    }
}
