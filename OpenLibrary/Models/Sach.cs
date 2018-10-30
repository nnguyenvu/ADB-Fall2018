using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Models
{
    public class Sach
    {
        public int _id { get; set; }

        [BsonElement("ten_sach")]
        public string TenSach { get; set; }
        [BsonElement("id_tac_gia")]
        public int IdTacGia { get; set; }
        [BsonElement("doc_thu")]
        public string DocThu { get; set; }
        [BsonElement("id_loai_sach")]
        public int IdLoaiSach { get; set; }
        [BsonElement("id_nha_xuat_ban")]
        public int IdNhaXuatBan { get; set; }
        [BsonElement("gioi_thieu")]
        public string GioiThieu { get; set; }
        [BsonElement("sku")]
        public string Sku { get; set; }
        [BsonElement("trong_luong")]
        public int TrongLuong { get; set; }
        [BsonElement("trang_thai")]
        public int TrangThai { get; set; }
        [BsonElement("hinh")]
        public string Hinh { get; set; }
        [BsonElement("don_gia")]
        public int DonGia { get; set; }
        [BsonElement("gia_bia")]
        public int GiaBia { get; set; }
        [BsonElement("noi_bat")]
        public int NoiBat { get; set; }

    }
}
