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
}
