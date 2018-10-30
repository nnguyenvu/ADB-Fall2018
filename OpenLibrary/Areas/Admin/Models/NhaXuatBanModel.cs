
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Areas.Admin.Models
{
    public class NhaXuatBanModel
    {
        private IMongoDatabase db { get; set; }
        private IMongoCollection<NhaXuatBan> collection { get; set; }
        public NhaXuatBanModel()
        {
            Dbconnect dbconnect = new Dbconnect();
            db = dbconnect.GetDB();
            collection = db.GetCollection<NhaXuatBan>("nha_xuat_ban");
        }

        public List<NhaXuatBan> DocTatCaNhaXuatBan()
        {
                 
            return collection.AsQueryable().OrderByDescending(x=>x._id).ToList();
        }

        public List<NhaXuatBan> DocNhaXuatBanTheoID(int id)
        {

            return collection.AsQueryable().Where(n => n._id == id).ToList();
        }

        public int ThemNhaXuatBan(string ten_nha_xuat_ban,string dia_chi,string dien_thoai,string email)
        {
            NhaXuatBan nhaXuatBan = new NhaXuatBan();
            nhaXuatBan._id = Counter.AI("id_nha_xuat_ban", db);
            nhaXuatBan.TenNhaXuatBan = ten_nha_xuat_ban;
            nhaXuatBan.DiaChi = dia_chi;
            nhaXuatBan.DienThoai = dien_thoai;
            nhaXuatBan.Email = email;
            collection.InsertOne(nhaXuatBan);
            

            var result = collection.AsQueryable().Where(n => n._id == nhaXuatBan._id).ToArray();
            //Nếu result.Count() = 1  là thêm được một collection.
            return result.Count();
        }

        public int CapNhatNhaXuatBanTheoID(int id, string ten_nha_xuat_ban,string dia_chi,string dien_thoai,string email)
        {

            var updatedef = Builders<NhaXuatBan>.Update.Set(n => n.TenNhaXuatBan, ten_nha_xuat_ban)
                                                       .Set(n => n.DiaChi, dia_chi)
                                                       .Set(n => n.DienThoai, dien_thoai)
                                                       .Set(n => n.Email, email);
            var result = collection.UpdateOne(n => n._id == id, updatedef).ModifiedCount;
            return (int) result;
        }

        public int XoaNhaXuatBanTheoID(int id)
        {
            return (int )collection.DeleteOne(n => n._id == id).DeletedCount;
        }

        public List<NhaXuatBan> SearchNhaXuatBan(string keyword)
        {
            collection.Indexes.CreateOne(Builders<NhaXuatBan>.IndexKeys.Text(x => x.TenNhaXuatBan));

            return collection.Find(Builders<NhaXuatBan>.Filter.Text(keyword)).ToList();
        }


    }
}
