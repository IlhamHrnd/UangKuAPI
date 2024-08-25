using UangKuAPI.BusinessObjects.Helper;

namespace EbookAPI.Encryptor
{
    public class Encryptor
    {
        //Untuk Men Enkripsi Data
        public static string DataEncrypt(string? enkripsi, string? key01, string? key02, string? key03)
        {
            if (string.IsNullOrEmpty(enkripsi))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(key01) || string.IsNullOrEmpty(key02) || string.IsNullOrEmpty(key03))
            {
                return string.Empty;
            }

            var firstEncrypt = SecureAES.DataEncrypt(enkripsi, key01);
            var secondEncrypt = SecureCrypto.Crypto_Encrypt(firstEncrypt, key02, key03);
            return secondEncrypt;
        }

        public static string OldDataEncrypt(string? enkripsi, string? key01)
        {
            if (string.IsNullOrEmpty(enkripsi))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(key01))
            {
                return string.Empty;
            }

            var firstEncrypt = SecureAES.DataEncrypt(enkripsi, key01);
            return firstEncrypt;
        }

        //Untuk Men Dekripsi Data
        public static string DataDecrypt(string? dekripsi, string? key01, string? key02, string? key03)
        {
            if (string.IsNullOrEmpty(dekripsi))
            {
                return string.Empty;
            }

            try
            {
                if (string.IsNullOrEmpty(key01) || string.IsNullOrEmpty(key02) || string.IsNullOrEmpty(key03))
                {
                    return string.Empty;
                }

                var firstDecrypt = SecureCrypto.Crypto_Decrypt(dekripsi, key02, key03);
                var secondDecrypt = SecureAES.DataDecrypt(firstDecrypt, key01);
                return secondDecrypt;
            }
            catch
            {
                if (string.IsNullOrEmpty(key01))
                {
                    return string.Empty;
                }

                var firstDecrypt = SecureAES.DataDecrypt(dekripsi, key01);
                return firstDecrypt;
            }
        }
    }
}
