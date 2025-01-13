namespace Web_DonNghiPhep.ViewModels
{
    public class PagingViewModel
    {
        public int? CurrentPage { get; set; }
        public int? TotalPages { get; set; }

        public string Action { get; set; } = "Index";

        public string Filter { get; set; } = string.Empty;
    }
}
