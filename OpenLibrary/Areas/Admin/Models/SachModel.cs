using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Areas.Admin.Models
{
    public  class SachModel
    {
        private IMongoDatabase db { get; set; }
        private IMongoCollection<Sach> collection { get; set; }

        public SachModel()
        {
            Dbconnect dbconnect = new Dbconnect();
            db = dbconnect.GetDB();
            collection = db.GetCollection<Sach>("sach");
        }

        public List<Sach> DocTatCaSach()
        {
            return collection.AsQueryable().OrderByDescending(s => s._id).ToList();
        }

        public List<Sach> DocSachTheoID(int id)
        {
            return collection.AsQueryable().Where(s => s._id == id).ToList();
        }

        public int ThemSach(string ten_sach,int id_tac_gia,int id_loai_sach,
                            int id_nha_xuat_ban,string gioi_thieu,int trong_luong,
                            int trang_thai,string hinh,int don_gia,int gia_bia,int noi_bat)
        {
            Sach sach  = new Sach();
            sach._id            = Counter.AI("sach_id", db);
            sach.TenSach        = ten_sach;
            sach.IdTacGia       = id_tac_gia;
            sach.IdLoaiSach     = id_loai_sach;
            sach.IdNhaXuatBan   = id_nha_xuat_ban;
            sach.GioiThieu      = gioi_thieu;
            sach.TrongLuong     = trong_luong;
            sach.TrangThai      = trang_thai;
            sach.Hinh           = hinh;
            sach.DonGia         = don_gia;
            sach.GiaBia         = gia_bia;
            sach.NoiBat         = noi_bat;

            collection.InsertOne(sach);
            int result = collection.AsQueryable().Where(s => s._id == sach._id).ToArray().Count();
            //Nếu trả về 1 là thêm được 1 tác giả
            return result;
        }

        public int CapNhatSachTheoID(int id , string ten_sach, int id_tac_gia,
                                     int id_loai_sach, int id_nha_xuat_ban,string gioi_thieu, 
                                     int trong_luong, int trang_thai, string hinh, 
                                     int don_gia, int gia_bia, int noi_bat )
        {
            var updatedef = Builders<Sach>.Update.Set(s => s.TenSach, ten_sach)
                                                 .Set(s => s.IdTacGia, id_tac_gia)
                                                 .Set(s => s.IdLoaiSach, id_loai_sach)
                                                 .Set(s => s.IdNhaXuatBan, id_nha_xuat_ban)
                                                 .Set(s => s.GioiThieu, gioi_thieu)
                                                 .Set(s => s.TrongLuong, trong_luong)
                                                 .Set(s => s.TrangThai, trang_thai)
                                                 .Set(s => s.Hinh, hinh)
                                                 .Set(s => s.DonGia, don_gia)
                                                 .Set(s => s.GiaBia, gia_bia)
                                                 .Set(s => s.NoiBat, noi_bat);

            var result =collection.UpdateOne(s => s._id == id, updatedef).ModifiedCount;
            //  .ModifiedCount trả về số document cập nhật được.
            return (int) result;

         }

        public int XoaSach(int id)
        {
            return (int) collection.DeleteOne(s => s._id == id).DeletedCount;
        }

        public List<Sach> SearchSach(string keyword)
        {
            
            return collection.Find(Builders<Sach>.Filter.Text(keyword)).ToList();
        }

        


    }
}
