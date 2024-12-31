namespace Web_DonNghiPhep.ViewModels
{
    public class ListNghiPhepVM
    {
        public string MaDon { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int SoNgay { get; set; }
        public string LyDo { get; set; }
        public string TrangThai { get; set; }
        public string TenTruongPhong { get; set; } // Tên trưởng phòng
        public DateTime? NgayXuLy { get; set; }    // Ngày xử lý (có thể null)
        public string LyDoTuChoi { get; set; }
    }
}
