using MongoDB.Driver.Linq;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace OpenLibrary.Models
{
    public class LoaiSachModel
    {
        private IMongoDatabase db { get; set; }
        private IMongoCollection<LoaiSach> collection { get; set; }
        public LoaiSachModel()
        {
            Dbconnect dbconnect = new Dbconnect();
            db = dbconnect.GetDB();
            collection = db.GetCollection<LoaiSach>("loai_sach");
        }
        public List<LoaiSach> DocTatCaLoaiSach()
        {
            List<LoaiSach> loaisachs = collection.AsQueryable<LoaiSach>().ToList();
            return loaisachs;
        }

        public List<LoaiSach> DocLoaiSachTheoId(int id)
        {
            List<LoaiSach> loaisachtheoids = collection.AsQueryable<LoaiSach>().Where(e => e._id == id).ToList();
            return loaisachtheoids;
        }

        public List<LoaiSach> DocLoaiSachTheoIdCha(int id_cha)
        {
            List<LoaiSach> loaisachtheoidchas = collection.AsQueryable<LoaiSach>().Where(e=>e.IdLoaiCha == id_cha).ToList();
            return loaisachtheoidchas;
        }

        public int ThemLoaiSach(string ten_loai_sach,int id_loai_cha,int trang_thai)
        {
            LoaiSach loaisach = new LoaiSach();
            loaisach._id = Counter.AI("loai_sach_id", db);
            loaisach.TenLoaiSach = ten_loai_sach;
            loaisach.IdLoaiCha = id_loai_cha;
            loaisach.TrangThai = trang_thai;

            collection.InsertOne(loaisach);
            var check = loaisach._id;

            //Nếu Count > 0 là thêm được
            var result = collection.AsQueryable().Where(x => x._id == check).ToArray().Count();
            return result;
        }


        public int XoaLoaiSachTheoID(int id)
        {
            collection.DeleteOne(x => x._id == id);
            var result = collection.AsQueryable().Where(x => x._id == id).ToArray().Count();

            //Nếu result == 0 là xóa được.
            return result;
        }

        public long CapNhatLoaiSachTheoID(int id,string ten_loai_sach,int id_loai_cha,int trang_thai)
        {
            var updatedef = Builders<LoaiSach>.Update.Set(x => x.TenLoaiSach,ten_loai_sach)
                                                     .Set(x=>x.IdLoaiCha,id_loai_cha)
                                                     .Set(x=>x.TrangThai,trang_thai);
            var update = collection.UpdateOne(x => x._id == id, updatedef);
            return (int) update.ModifiedCount; //Trả về số dòng thay đổi
        }
        
    }
}
