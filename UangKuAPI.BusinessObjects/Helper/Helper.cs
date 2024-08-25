using System.Security.Cryptography;
using System.Text;

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

    public static class AppConstant
    {
        private static string foundMsg = "Data Found";
        private static string notFoundMsg = "Data Not Found";

        public static string FoundMsg { get => foundMsg; set => foundMsg = value; }
        public static string NotFoundMsg { get => notFoundMsg; set => notFoundMsg = value; }
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

        public static byte[]? StringToByte(string? value)
        {
            var result = string.IsNullOrEmpty(value) 
                ? null 
                : Encoding.UTF8.GetBytes(value);
            return result;
        }
    }

    public class SecureAES
    {
        public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            byte[] saltBytes = new byte[] { 2, 1, 7, 3, 6, 4, 8, 5 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            byte[] saltBytes = new byte[] { 2, 1, 7, 3, 6, 4, 8, 5 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        //Untuk Men Enkripsi Data
        public static string DataEncrypt(string enkripsi, string key)
        {
            byte[] dataToBeEncrypted = Encoding.UTF8.GetBytes(enkripsi);
            byte[] dataBytes = Encoding.UTF8.GetBytes(key);

            dataBytes = SHA256.Create().ComputeHash(dataBytes);

            byte[] bytesEncrypted = AES_Encrypt(dataToBeEncrypted, dataBytes);

            return Convert.ToBase64String(bytesEncrypted);
        }

        //Untuk Men Dekripsi Data
        public static string DataDecrypt(string dekripsi, string key)
        {
            byte[] dataToBeDecrypted = Convert.FromBase64String(dekripsi);
            byte[] dataBytesdecrypt = Encoding.UTF8.GetBytes(key);
            dataBytesdecrypt = SHA256.Create().ComputeHash(dataBytesdecrypt);

            byte[] bytesDecrypted = AES_Decrypt(dataToBeDecrypted, dataBytesdecrypt);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }
    }

    public class SecureCrypto
    {
        public static string Crypto_Encrypt(string data, string key1, string key2)
        {
            var ms = new MemoryStream();

            var objKey = new DESCryptoServiceProvider { Key = objLockKey(key1), IV = objLockKey(key2) };

            var encStream = new CryptoStream(ms,
            objKey.CreateEncryptor(), CryptoStreamMode.Write);
            var sw = new StreamWriter(encStream);
            sw.WriteLine(data);
            sw.Close();
            encStream.Close();

            var bytData = ms.ToArray();
            var strReturnData = bytData.Aggregate("", (current, bytChar) => current + bytChar.ToString().PadLeft(3, Convert.ToChar("0")));

            ms.Close();

            return strReturnData;
        }

        public static string Crypto_Decrypt(string data, string key1, string key2)
        {
            var bytData = new byte[data.Length / 3];
            for (int i = 0; i < data.Length; i += 3)
            {
                bytData[i / 3] = byte.Parse(data.Substring(i, 3));
            }

            var ms = new MemoryStream(bytData);
            var objKey = new DESCryptoServiceProvider { Key = objLockKey(key1), IV = objLockKey(key2) };
            var encStream = new CryptoStream(ms, objKey.CreateDecryptor(), CryptoStreamMode.Read);
            var sr = new StreamReader(encStream);

            var strReturnData = sr.ReadLine();

            sr.Close();
            encStream.Close();
            ms.Close();

            return strReturnData;
        }

        private static byte[] objLockKey(string strPassword)
        {
            const int intKeyLength = 8;
            strPassword = strPassword.PadRight(intKeyLength,
            Convert.ToChar(".")).Substring(0, intKeyLength);
            var objKey = new byte[strPassword.Length];
            for (var intCount = 0; intCount < strPassword.Length; intCount++)
            {
                objKey[intCount] = Convert.ToByte(Convert.ToChar(strPassword.Substring(intCount, 1)));
            }
            return objKey;
        }
    }
}
