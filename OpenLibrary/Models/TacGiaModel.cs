
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Models
{
    public class TacGiaModel
    {
        private IMongoDatabase db { get; set; }
        private IMongoCollection<TacGia> collection { get; set; }
        public TacGiaModel()
        {
            Dbconnect dbconnect = new Dbconnect();
            db = dbconnect.GetDB();
            collection = db.GetCollection<TacGia>("tac_gia");
        }

        public List<TacGia> DocTatCaTacGia()
        {
            return collection.AsQueryable().ToList();
        }

        public List<TacGia> DocTacGiaTheoId(int id)
        {
            return collection.AsQueryable().Where(x => x._id == id).ToList();
        }

        public int ThemTacGia(string ten_tac_gia,string ngay_sinh ,string gioi_thieu)
        {
            TacGia tg = new TacGia();
            tg._id = Counter.AI("id_tac_gia", db);
            tg.TenTacGia = ten_tac_gia;
            tg.NgaySinh = ngay_sinh;
            tg.GioiThieu = gioi_thieu;
            collection.InsertOne(tg);

            var result = collection.AsQueryable().Where(t => t._id == tg._id).ToArray().Count();
            //Nếu result > 0 là thêm được, result =1 
            return result;
        }

        public int XoaTacGia(int id)
        {
            return (int)collection.DeleteOne(x => x._id == id).DeletedCount;
        }

        public int CapNhatTacGiaTheoID(int id, string ten_tac_gia, string ngay_sinh, string gioi_thieu)
        {
            var updatedef = Builders<TacGia>.Update.Set(t => t.TenTacGia, ten_tac_gia)
                                                   .Set(t => t.NgaySinh, ngay_sinh)
                                                   .Set(t => t.GioiThieu, gioi_thieu);

            var updatereuslt =collection.UpdateOne(t => t._id == id, updatedef);
            return (int) updatereuslt.ModifiedCount;
        }
    }
}
