using System.Security.Cryptography;
using System.Text;

namespace UangKuAPI.BusinessObjects.Base
{
    public class Encryptor
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
        public static string DataEncrypt(string? value, string key)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            byte[] dataToBeEncrypted = Encoding.UTF8.GetBytes(value);
            byte[] dataBytes = Encoding.UTF8.GetBytes(key);

            dataBytes = SHA256.Create().ComputeHash(dataBytes);

            byte[] bytesEncrypted = AES_Encrypt(dataToBeEncrypted, dataBytes);

            return Convert.ToBase64String(bytesEncrypted);
        }

        //Untuk Men Dekripsi Data
        public static string DataDecrypt(string? value, string key)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;

            byte[] dataToBeDecrypted = Convert.FromBase64String(value);
            byte[] dataBytesdecrypt = Encoding.UTF8.GetBytes(key);
            dataBytesdecrypt = SHA256.Create().ComputeHash(dataBytesdecrypt);

            byte[] bytesDecrypted = AES_Decrypt(dataToBeDecrypted, dataBytesdecrypt);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }
    }
}
