using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Models
{
    public static class MyExtension
    {
        public static string GetTenTacGiaFromTacGiaCollection(this int id)
        {
            TacGiaModel tacGiaModel = new TacGiaModel();
            TacGia tg = tacGiaModel.DocTacGiaTheoId(id).First();
            return tg.TenTacGia;
        }

        public static string GetTenNhaXuatBanFromNhaXuatBanCollection(this int id)
        {
            NhaXuatBanModel nhaXuatBanModel = new NhaXuatBanModel();
            return nhaXuatBanModel.DocNhaXuatBanTheoID(id).First().TenNhaXuatBan;
        }
        
        public static string GetTenLoaiSachFromLoaiSachCollection(this int id)
        {
            LoaiSachModel loaiSachModel = new LoaiSachModel();
            return loaiSachModel.DocLoaiSachTheoId(id).First().TenLoaiSach;
        }
    }
}
