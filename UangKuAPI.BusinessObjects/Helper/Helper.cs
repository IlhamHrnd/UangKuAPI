namespace UangKuAPI.BusinessObjects.Helper
{
    public class Helper
    {
        //Class  Untuk Parameter
        //Jangan DIGANTI
        public class AppParameterValue
        {
            private static string? maxpicture = "MaxPicture";
            public static string? MaxPicture { get => maxpicture; set => maxpicture = value; }
            private static string? maxsize = "MaxFileSize";
            public static string? MaxSize { get => maxsize; set => maxsize = value; }
        }

        public static string NumberingFormat(int number, string format)
        {
            var result = number.ToString(format);
            return result;
        }
    }

    public class DateFormat
    {
        public static string DateTimeNow(string format, DateTime? dateTime)
        {
            DateTime _dateTime = dateTime.HasValue ? dateTime.Value : DateTime.Now;
            string _format = !string.IsNullOrEmpty(format) ? _dateTime.ToString(format) : _dateTime.ToString();
            return _format;
        }

        public static DateTime DateTimeNow(DateTime? dateTime)
        {
            var result = dateTime.HasValue
                ? dateTime.Value 
                : DateTime.Now;
            return result;
        }

        public static DateTime DateTimeNowDate(int year, int month, int day)
        {
            var result = new DateTime(year, month, day);
            return result;
        }

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

    public static class Converter
    {
        public static long IntToLong(int data)
        {
            long result = data * 1024 * 1024;
            return result;
        }

        public static DateTime StringToDateTime(string value)
        {
            DateTime result;
            if (string.IsNullOrEmpty(value))
            {
                result = DateTime.Now;
            }
            else
            {
                try
                {
                    result = DateTime.Parse(value);
                }
                catch
                {
                    result = DateTime.Now;
                }
            }

            return result;
        }
    }
}
