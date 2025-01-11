namespace Web_DonNghiPhep.Helpers
{
    public class HolidayHelper
    {
        public static List<(int Day, int Month)> GetRecurringHolidays()
        {
            return new List<(int Day, int Month)>
            {
                (1, 1),  // Tết Dương Lịch
                (30, 4), // Ngày Giải phóng miền Nam
                (1, 5)   // Quốc tế Lao động
            };
        }

        public static bool IsHoliday(DateTime date)
        {
            var holidays = GetRecurringHolidays();
            return holidays.Any(h => h.Day == date.Day && h.Month == date.Month);
        }
    }
}
