using System.Text;

namespace UangKuAPI.BusinessObjects.Base
{
    public class DateFormat
    {
        public static string DateTimeNow(string format, DateTime? dateTime)
        {
            DateTime _dateTime = dateTime ?? DateTime.Now;
            string _format = !string.IsNullOrEmpty(format) ? _dateTime.ToString(format) : _dateTime.ToString();
            return _format;
        }

        public static DateTime DateTimeNow(DateTime? dateTime)
        {
            var result = dateTime ?? DateTime.Now;
            return result;
        }

        public static DateTime DateTimeNow()
        {
            var result = DateTimeNow(null);
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

    public static class AppConstant
    {
        private static string foundMsg = "Data Found";
        private static string notFoundMsg = "Data Not Found";
        private static string requiredMsg = "{0} Is Required";
        private static string alreadyexistMsg = "{0} Already Exist";
        private static string createdsuccessMsg = "{0} {1} Created Successfully";
        private static string updatesuccessMsg = "{0} Updated Successfully";
        private static string failedMsg = "Failed To {0} Data For {1} {2}";

        public static string FoundMsg { get => foundMsg; set => foundMsg = value; }
        public static string NotFoundMsg { get => notFoundMsg; set => notFoundMsg = value; }
        public static string RequiredMsg { get => requiredMsg; set => requiredMsg = value; }
        public static string AlreadyExistMsg { get => alreadyexistMsg; set => alreadyexistMsg = value; }
        public static string CreatedSuccessMsg { get => createdsuccessMsg; set => createdsuccessMsg = value; }
        public static string UpdateSuccessMsg { get => updatesuccessMsg; set => updatesuccessMsg = value; }
        public static string FailedMsg { get => failedMsg; set => failedMsg = value; }
    }

    public static class Converter
    {
        public static int StringToInt(string dataString, int dataInt)
        {
            int result = !string.IsNullOrEmpty(dataString) ? int.Parse(dataString) : dataInt;

            return result;
        }

        public static string IntToString(int data)
        {
            var result = data.ToString();
            return result;
        }

        public static long IntToLong(int data)
        {
            long result = data * 1024 * 1024;
            return result;
        }

        public static int DecimalToInt(decimal value)
        {
            var values = (int)value;
            return values;
        }

        public static string DecimalToString(decimal value)
        {
            try
            {
                int result = (int)Math.Round(value);
                var values = result.ToString();
                return values;
            }
            catch (OverflowException)
            {
                return "Value too large";
            }
        }

        public static bool StringToBool(string value, bool defaultValue)
        {
            bool result;

            try
            {
                result = Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;
        }

        public static DateTime StringToDateTime(string value, DateTime defaultValue)
        {
            DateTime result;

            try
            {
                result = DateTime.Parse(value);
            }
            catch
            {
                result = defaultValue;
            }

            return result;
        }

        //Class Untuk Convert Byte[] Ke Base64String
        public static string ByteToStringImg(byte[] imgPath)
        {
            try
            {
                string imgString = Convert.ToBase64String(imgPath);
                return imgString;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Class Untuk Convert Base64String Ke Byte[]
        public static byte[] StringToByteImg(string imgPath)
        {
            byte[] imgByte = Array.Empty<byte>();
            try
            {
                imgByte = Convert.FromBase64String(imgPath);
                return imgByte;
            }
            catch (Exception)
            {
                return imgByte;
            }
        }

        //Function Untuk Convert PDF Ke Byte[]
        public static byte[] PDFToByte(string filePath)
        {
            byte[] pdfBytes = File.ReadAllBytes(filePath);
            return pdfBytes;
        }

        //Class Untuk Decode Base64 Ke Byte[]
        public static byte[] DecodeBase64ToBytes(string base64String)
        {
            byte[] data = Array.Empty<byte>();
            try
            {
                data = Convert.FromBase64String(base64String);
                return data;
            }
            catch (Exception)
            {
                return data;
            }
        }

        //Class Untuk Decode Base64 Ke String
        public static string DecodeBase64ToString(string base64String)
        {
            try
            {
                byte[] data = Convert.FromBase64String(base64String);
                string decodedString = Encoding.UTF8.GetString(data);
                return decodedString;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Function Untuk StringBuilder
        public static string BuilderString(params object[] items)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < items.Length; i++)
            {
                builder.Append(items[i]);
            }
            var result = builder.ToString();
            return result;
        }

        //Function Untuk IList Jadi List
        public static List<T> ConvertIListToList<T>(IList<T> inputList)
        {
            var list = new List<T>(inputList);
            return list;
        }
    }
}
