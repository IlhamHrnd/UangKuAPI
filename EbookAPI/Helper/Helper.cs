using Microsoft.EntityFrameworkCore;
using UangKuAPI.Context;
using static UangKuAPI.BusinessObjects.Helper.DateFormat;

namespace UangKuAPI.Helper
{
    public class ParameterHelper : BusinessObjects.Helper.Helper
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

    public static class DateFormat
    {
        public static string DateTimeNow(string format, DateTime dateTime)
        {
            string result = BusinessObjects.Helper.DateFormat.DateTimeNow(format, dateTime);
            return result;
        }

        public static DateTime DateTimeNowDate(int year, int month, int day)
        {
            var result = BusinessObjects.Helper.DateFormat.DateTimeNowDate(year, month, day);
            return result;
        }

        public static DateTime DateTimeNow()
        {
            var result = BusinessObjects.Helper.DateFormat.DateTimeNow(null);
            return result;
        }

        public static string DateTimeIsNull(DateTime? dateTime)
        {
            string result = BusinessObjects.Helper.DateFormat.DateTimeNow(Yearmonthdate, dateTime);
            return result;
        }
    }
}
