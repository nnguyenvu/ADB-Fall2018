using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Areas.Admin.Models
{
    public class AddViewModel
    {
        public int IdSach { get; set; }
        public int  IdTacGia {get;set;}
        public List<TacGia> listTacGia { get; set; }
        public int IdLoaiSach { get; set; }
        public List<LoaiSach> listLoaiSach { get; set; }
        public int IdNhaXuatBan { get; set; }
        public List<NhaXuatBan> listNhaXuatBan { get; set; }
        public string TenSach { get; set; }
        public string GioiThieu { get; set; }
        public int TrongLuong { get; set; }
        public int DonGia { get; set; }
        public int GiaBia { get; set; }
        public bool TrangThai { get; set; }
        public bool NoiBat { get; set; }
        public IFormFile image { get; set; }
        public string Hinh { get; set; }

        //Tác Giả
        public string TenTacGia { get; set; }
        public string NgaySinhTacGia { get; set; }


        //Nhà xuất bản
        public string TenNhaXuatBan { get; set; }
        public string DiaChiNhaXuatBan { get; set; }
        public string DienThoaiNhaXuatBan { get; set; }
        public string EmailNhaXuatBan { get; set; }

        //Loại sách
        public string TenLoaiSach { get; set; }
        public int IdLoaiSachCha { get; set; }

    }
}
