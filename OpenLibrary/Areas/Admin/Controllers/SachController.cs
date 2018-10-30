using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.Areas.Admin.Models;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using X.PagedList;

namespace OpenLibrary.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SachController : Controller
    {
        private readonly IHostingEnvironment _environment;
        public SachController(IHostingEnvironment IHostingEnvironment)
        {
            _environment = IHostingEnvironment;
        }
        

        public IActionResult Index(int? page,string searchString)
        {
            ViewData["title_table"] = "Sách";
            ViewBag.searchString = searchString;
            SachModel sachModel = new SachModel();
            List<Sach> sachs;
            if(searchString != null)
            {
                sachs = sachModel.SearchSach(searchString);
            }
            else
            {
                sachs = sachModel.DocTatCaSach();
            }
            var pageNumber = page ?? 1;
            int limit = 5;
            var onePageofSachs = sachs.ToPagedList(pageNumber, limit);

            ViewBag.hasSearchResult = (sachs.Count() > 0) ? true : false;
            ViewBag.hasPagination = (sachs.Count() > limit) ? true : false;

            return View(onePageofSachs);
        }
        

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["title_table"] = "Thêm Sách";
            AddViewModel addViewModel = new AddViewModel();
            TacGiaModel tacGiaModel = new TacGiaModel();
            LoaiSachModel loaiSachModel = new LoaiSachModel();
            NhaXuatBanModel nhaXuatBanModel = new NhaXuatBanModel();
            addViewModel.listTacGia = tacGiaModel.DocTatCaTacGia();
            addViewModel.listLoaiSach = loaiSachModel.DocTatCaLoaiSach();
            addViewModel.listNhaXuatBan = nhaXuatBanModel.DocTatCaNhaXuatBan();
            return View(addViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddViewModel addViewModel)
        {
            bool flag = false;
            

            var newFilename = string.Empty;
            string[] arrImageTypePermission = { "image/png", "image/jpg", "image/jpeg", "image/gif" };
            if (addViewModel.image != null && arrImageTypePermission.Contains(addViewModel.image.ContentType))
            {
                flag = true;
                var fileName = string.Empty;
                string PathDB = string.Empty;
                var file = addViewModel.image;
                if (file.Length > 0)
                {
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    // Assigning Unique Filename(Guid)
                    var uniqueFileName = Convert.ToString(Guid.NewGuid());
                    //Getting file Extension
                    var FileExtension = Path.GetExtension(fileName);
                    // concating  FileName + FileExtension
                    newFilename = uniqueFileName + FileExtension;
                    // Combines two strings into a path.
                    fileName = Path.Combine(_environment.WebRootPath, "images_books") + $@"\{newFilename}";
                    PathDB = "Upload/" + newFilename;
                    using (FileStream fs = System.IO.File.Create(fileName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                }

            }
            if (flag)
            {
                
                string ten_sach = addViewModel.TenSach;
                int id_tac_gia  = addViewModel.IdTacGia;
                int id_loai_sach = addViewModel.IdLoaiSach;
                int id_nha_xuat_ban = addViewModel.IdNhaXuatBan;
                string gioi_thieu = addViewModel.GioiThieu;
                int trong_luong = addViewModel.TrongLuong;
                int don_gia = addViewModel.DonGia;
                int gia_bia = addViewModel.GiaBia;
                string hinh = newFilename;
                int trang_thai = (addViewModel.TrangThai) ? 1 : 0;
                int noi_bat = (addViewModel.NoiBat) ? 1 : 0;

                SachModel sachModel = new SachModel();
                int res = sachModel.ThemSach(ten_sach, id_tac_gia, id_loai_sach, id_nha_xuat_ban, gioi_thieu, trong_luong, trang_thai, hinh, don_gia, gia_bia, noi_bat);
                if (res == 1)
                {
                    return RedirectToAction("Index"); //Quay về trang danh sách

                }
                else
                {
                    return Content("ADD FAIL!");
                }
                
            }
            else
            {
                return Content("Fail!");
            }

        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["title_table"] = "Cập Nhật Sách";
            AddViewModel addViewModel = new AddViewModel();
            TacGiaModel tacGiaModel = new TacGiaModel();
            LoaiSachModel loaiSachModel = new LoaiSachModel();
            NhaXuatBanModel nhaXuatBanModel = new NhaXuatBanModel();
            SachModel sachModel = new SachModel();
            addViewModel.listTacGia = tacGiaModel.DocTatCaTacGia();
            addViewModel.listLoaiSach = loaiSachModel.DocTatCaLoaiSach();
            addViewModel.listNhaXuatBan = nhaXuatBanModel.DocTatCaNhaXuatBan();

            Sach sach = sachModel.DocSachTheoID(id).First();
            addViewModel.IdSach = sach._id;
            addViewModel.TenSach = sach.TenSach;
            addViewModel.IdTacGia = sach.IdTacGia;
            addViewModel.IdLoaiSach = sach.IdLoaiSach;
            addViewModel.IdNhaXuatBan = sach.IdNhaXuatBan;
            addViewModel.GioiThieu = sach.GioiThieu;
            addViewModel.TrongLuong = sach.TrongLuong;
            addViewModel.DonGia = sach.DonGia;
            addViewModel.GiaBia = sach.GiaBia;
            addViewModel.Hinh = sach.Hinh;
            addViewModel.TrangThai = (sach.TrangThai == 1) ? true : false;
            addViewModel.NoiBat = (sach.NoiBat == 1) ? true : false;

            return View(addViewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddViewModel addViewModel)
        {
            bool flag = false;
            bool flag_chon_anh = false; // co chon anh
            string hinh = string.Empty;
            var newFilename = string.Empty;
            string[] arrImageTypePermission = { "image/png", "image/jpg", "image/jpeg", "image/gif" };
            if (addViewModel.image != null && arrImageTypePermission.Contains(addViewModel.image.ContentType))
            {
                flag = true;
                var fileName = string.Empty;
                string PathDB = string.Empty;
                var file = addViewModel.image;
                if (file.Length > 0)
                {
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    // Assigning Unique Filename(Guid)
                    var uniqueFileName = Convert.ToString(Guid.NewGuid());
                    //Getting file Extension
                    var FileExtension = Path.GetExtension(fileName);
                    // concating  FileName + FileExtension
                    newFilename = uniqueFileName + FileExtension;
                    // Combines two strings into a path.
                    fileName = Path.Combine(_environment.WebRootPath, "images_books") + $@"\{newFilename}";
                    PathDB = "Upload/" + newFilename;

                    //Upload ảnh vào đường dẫn ~/wwwroot/images_books/{newFilebname}
                    using (FileStream fs = System.IO.File.Create(fileName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }

                    //gán ảnh
                    hinh = newFilename;

                    //Remove ảnh trong đường dẫn ~/wwwroot/images_books/{ten_file_cu}
                    string duong_dan_file_anh_cu = Path.Combine(_environment.WebRootPath, "images_books") + $@"\{addViewModel.Hinh}";
                    if (System.IO.File.Exists(duong_dan_file_anh_cu))
                    {
                        System.IO.File.Delete(duong_dan_file_anh_cu);
                    }
                }

            }
            else
            {//Khi không có chọn ảnh thì lưu lại hình cũ từ AddViewModel.Hinh
                flag_chon_anh = true;  // không có chọn ảnh
                hinh = addViewModel.Hinh;
            }
           
            if (flag == true || flag_chon_anh == true)
            {
                int id = addViewModel.IdSach;
                string ten_sach = addViewModel.TenSach;
                int id_loai_sach = addViewModel.IdLoaiSach;
                int id_tac_gia = addViewModel.IdTacGia;
                int id_nha_xuat_ban = addViewModel.IdNhaXuatBan;
                string gioi_thieu = addViewModel.GioiThieu;
                int trong_luong = addViewModel.TrongLuong;
                int don_gia = addViewModel.DonGia;
                int gia_bia = addViewModel.GiaBia;
                int trang_thai = addViewModel.TrangThai ? 1 : 0;
                int noi_bat = addViewModel.NoiBat ? 1 : 0;

                SachModel sachModel = new SachModel();
                int res = sachModel.CapNhatSachTheoID(id, ten_sach, id_tac_gia, id_loai_sach, id_nha_xuat_ban, gioi_thieu, trong_luong, trang_thai, hinh, don_gia, gia_bia, noi_bat);
                if (res == 1) return RedirectToAction("Index");
            }
            return Content("Fail Update!");
            
        }
           
      

        public IActionResult Delete(int id)
        {
            SachModel sachModel = new SachModel();
            Sach sach = sachModel.DocSachTheoID(id).First();
            int res = sachModel.XoaSach(id);
            if (res == 1)
            {
                string fullPath = Path.Combine(_environment.WebRootPath, "images_books") + $@"\{sach.Hinh}";
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return Content("Loi Xoa");
            }
        }

        
    }
}