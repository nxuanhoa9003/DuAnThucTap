using Microsoft.AspNetCore.Mvc;
using Web_DonNghiPhep.Data;
using Web_DonNghiPhep.Services;
using Web_DonNghiPhep.ViewModels;

namespace Web_DonNghiPhep.Controllers
{
    public class ListDonDuyetController : Controller
    {
        private readonly MyDBContext _context;
        private readonly IMessageService _messageService;
        public ListDonDuyetController(MyDBContext context, IMessageService messageService)
        {
            _context = context;
            _messageService = messageService;
        }
        // LỊCH SỬ ĐƠN DUYỆT
        public IActionResult Index()
        {
            var danhSach = new List<ListNghiPhepVM>
            {
                new ListNghiPhepVM
                {
                    MaDon = "001",
                    NgayBatDau = new DateTime(2024, 12, 1),
                    NgayKetThuc = new DateTime(2024, 12, 3),
                    SoNgay = 3,
                    LyDo = "Nghỉ phép",
                    TrangThai = "Đang chờ",
                    TenTruongPhong = "Nguyễn Văn A",
                    NgayXuLy = null,
                    LyDoTuChoi = null
                },
                new ListNghiPhepVM
                {
                    MaDon = "002",
                    NgayBatDau = new DateTime(2024, 11, 10),
                    NgayKetThuc = new DateTime(2024, 11, 11),
                    SoNgay = 2,
                    LyDo = "Việc cá nhân",
                    TrangThai = "Đã duyệt",
                    TenTruongPhong = "Trần Thị B",
                    NgayXuLy = new DateTime(2024, 11, 10),
                    LyDoTuChoi = null
                },
                new ListNghiPhepVM
                {
                    MaDon = "003",
                    NgayBatDau = new DateTime(2024, 11, 15),
                    NgayKetThuc = new DateTime(2024, 11, 15),
                    SoNgay = 1,
                    LyDo = "Bệnh",
                    TrangThai = "Từ chối",
                    TenTruongPhong = "Phạm Văn C",
                    NgayXuLy = new DateTime(2024, 11, 15),
                    LyDoTuChoi = "Không phù hợp"
                }
            };

            return View(danhSach); // Truyền dữ liệu vào View
        }
        public IActionResult ChiTiet(string maDon)
        {
            // Dữ liệu mẫu (trong thực tế bạn sẽ truy vấn từ database)
            var listNghiPhep = new List<ListNghiPhepVM>
    {
        new ListNghiPhepVM
        {
            MaDon = "001",
            NgayBatDau = new DateTime(2024, 12, 1),
            NgayKetThuc = new DateTime(2024, 12, 3),
            SoNgay = 3,
            LyDo = "Nghỉ phép",
            TrangThai = "Đang chờ",
            TenTruongPhong = "Nguyễn Văn A",
            NgayXuLy = null,
            LyDoTuChoi = null
        },
        new ListNghiPhepVM
        {
            MaDon = "002",
            NgayBatDau = new DateTime(2024, 11, 10),
            NgayKetThuc = new DateTime(2024, 11, 11),
            SoNgay = 2,
            LyDo = "Việc cá nhân",
            TrangThai = "Đã duyệt",
            TenTruongPhong = "Trần Thị B",
            NgayXuLy = new DateTime(2024, 11, 10),
            LyDoTuChoi = null
        },
        new ListNghiPhepVM
        {
            MaDon = "003",
            NgayBatDau = new DateTime(2024, 11, 15),
            NgayKetThuc = new DateTime(2024, 11, 15),
            SoNgay = 1,
            LyDo = "Bệnh",
            TrangThai = "Từ chối",
            TenTruongPhong = "Phạm Văn C",
            NgayXuLy = new DateTime(2024, 11, 15),
            LyDoTuChoi = "Không phù hợp"
        }
    };

            // Tìm đơn nghỉ phép theo MaDon
            var nghiPhep = listNghiPhep.FirstOrDefault(x => x.MaDon == maDon);

            if (nghiPhep == null)
            {
                return NotFound("Không tìm thấy đơn nghỉ phép.");
            }

            return View(nghiPhep); // Truyền dữ liệu sang View ChiTiet
        }
    }
}
