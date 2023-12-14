using EbookAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace UangKuAPI.Helper
{
    public class ImageHelper
    {
        public static long MaxSizeInt(int data)
        {
            long result = data * 1024 * 1024;
            return result;
        }
    }
    public class ParameterHelper
    {
        private readonly AppDbContext _context;

        public ParameterHelper(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string?> GetParameterValue(string? appValue)
        {
            var parameter = await _context.Parameter
                .FirstOrDefaultAsync(p => p.ParameterID == appValue);

            return parameter?.ParameterValue;
        }

        public static int TryParseInt(string? value)
        {
            return int.TryParse(value, out int result) ? result : 0;
        }
    }

    //Class Static Untuk Parameter
    //Jangan DIGANTI
    public class AppParameterValue
    {
        private static string? maxpicture = "MaxPicture";
        public static string? MaxPicture { get => maxpicture; set => maxpicture = value; }
        private static string? maxsize = "MaxFileSize";
        public static string? MaxSize { get => maxsize; set => maxsize = value; }
    }

    public static class DateTimeFormat
    {
        public static string DateTimeNow(string format)
        {
            var result = DateTime.Now.ToString(format);

            return result;
        }

        public static string DateTimeUser(DateTime date, string format)
        {
            var result = date.ToString(format);

            return result;
        }

        public static string DateTimeStartMonth(int day, string format)
        {
            var dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, day);
            var result = dateTime.ToString(format);

            return result;
        }
    }

    public static class DateStringFormat
    {
        private static string yymmdd = "yyMMdd";
        public static string Yymmdd { get => yymmdd; set => yymmdd = value; }
        private static string yymmddhhmmss = "yyyy-MM-dd HH:mm:ss";
        public static string Yymmddhhmmss { get => yymmddhhmmss; set => yymmddhhmmss = value; }
        private static string yymmdd2 = "yyyy-MM-dd";
        public static string Yymmddhh2 { get => yymmdd2; set => yymmdd2 = value; }
    }

    public static class NumberStringFormat
    {
        public static string NumberThreeDigit(int number)
        {
            var result = number.ToString($"D3");

            return result;
        }
    }
}
