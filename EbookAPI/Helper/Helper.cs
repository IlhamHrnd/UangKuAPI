using EbookAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace UangKuAPI.Helper
{
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

    public static class DateFormat
    {
        public static string DateTimeNow(string format, DateTime dateTime)
        {
            string result = string.IsNullOrEmpty(format) ? dateTime.ToString() : dateTime.ToString(format);

            return result;
        }

        public static DateTime DateTimeNowDate(int year, int month, int day)
        {
            var result = new DateTime(year, month, day);

            return result;
        }

        public static DateTime DateTimeNow()
        {
            var result = DateTime.Now;
            return result;
        }

        public static string DateTimeIsNull(DateTime? dateTime, DateTime defaultTime)
        {
            string result = dateTime.HasValue
                ? DateFormat.DateTimeNow(DateStringFormat.Yearmonthdate, (DateTime)dateTime)
                : DateFormat.DateTimeNow(DateStringFormat.Yearmonthdate, defaultTime);
            return result;
        }
    }

    public static class DateStringFormat
    {
        private static string shortyearpattern = "yyMMdd";
        private static string longyearpattern = "yyyy-MM-dd HH:mm:ss";
        private static string yearmonthdate = "yyyy-MM-dd";
        private static string date = "dd/MM/yyyy";
        private static string datetime = "dd/MM/yyyy HH:mm";
        private static string datetimesecond = "dd/MM/yyyy HH:mm:ss";
        private static string datelong = "dd MMM yyyy";
        private static string dateshortmonth = "dd-MMM-yyyy";
        private static string longdatepattern = "dd-MMM-yyyy HH:mm:ss";
        private static string datehourminute = "dd/MM/yyyy HH:mm";
        private static string dateshortmonthhourminute = "dd-MMM-yyyy HH:mm";
        private static string hourmin = "HH:mm";
        private static string month = "MMMM";
        private static string daydatemonthyear = "dddd, dd MMMM yyyy";
        public static string Shortyearpattern { get => shortyearpattern; set => shortyearpattern = value; }
        public static string Longyearpattern { get => longyearpattern; set => longyearpattern = value; }
        public static string Date { get => date; set => date = value; }
        public static string Datetime { get => datetime; set => datetime = value; }
        public static string Datetimesecond { get => datetimesecond; set => datetimesecond = value; }
        public static string Datelong { get => datelong; set => datelong = value; }
        public static string Dateshortmonth { get => dateshortmonth; set => dateshortmonth = value; }
        public static string Longdatepattern { get => longdatepattern; set => longdatepattern = value; }
        public static string Datehourminute { get => datehourminute; set => datehourminute = value; }
        public static string Dateshortmonthhourminute { get => dateshortmonthhourminute; set => dateshortmonthhourminute = value; }
        public static string Hourmin { get => hourmin; set => hourmin = value; }
        public static string Month { get => month; set => month = value; }
        public static string Yearmonthdate { get => yearmonthdate; set => yearmonthdate = value; }
        public static string Daydatemonthyear { get => daydatemonthyear; set => daydatemonthyear = value; }
    }

    public static class NumberStringFormat
    {
        public static string NumberThreeDigit(int number)
        {
            var result = number.ToString($"D3");

            return result;
        }
    }

    public static class Converter
    {
        public static long IntToLong(int data)
        {
            long result = data * 1024 * 1024;

            return result;
        }
    }
}
