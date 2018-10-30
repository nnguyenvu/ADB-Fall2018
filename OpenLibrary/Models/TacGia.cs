using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Models
{
    public class TacGia
    {
        public Int32 _id { get; set; }
        [BsonElement("ten_tac_gia")]
        public string TenTacGia { get; set; }
        [BsonElement("ngay_sinh")]
        public string NgaySinh { get; set; }
        [BsonElement("gioi_thieu")]
        public string GioiThieu { get; set; }
    }
}
