using UangKuAPI.BusinessObjects.Filter;

namespace EbookAPI.Encryptor
{
    public class Encryptor
    {
        private static string _key01 = string.Empty;
        private static string _key02 = string.Empty;
        private static string _key03 = string.Empty;

        public Encryptor(IConfiguration config)
        {
            var param = config.GetRequiredSection("Parameter").Get<Parameter>();
            _key01 = param.Key01;
            _key02 = param.Key02;
            _key03 = param.Key03;
        }

        //Untuk Men Enkripsi Data
        public static string DataEncrypt(string? enkripsi)
        {
            if (string.IsNullOrEmpty(enkripsi))
            {
                return enkripsi;
            }
            var firstEncrypt = UangKuAPI.BusinessObjects.Helper.SecureAES.DataEncrypt(enkripsi, _key01);
            var secondEncrypt = UangKuAPI.BusinessObjects.Helper.SecureCrypto.Crypto_Encrypt(firstEncrypt, _key02, _key03);
            return secondEncrypt;
        }

        //Untuk Men Dekripsi Data
        public static string DataDecrypt(string? dekripsi)
        {
            if (string.IsNullOrEmpty(dekripsi))
            {
                return dekripsi;
            }
            var firstDecrypt = UangKuAPI.BusinessObjects.Helper.SecureCrypto.Crypto_Decrypt(dekripsi, _key02, _key03);
            var secondDecrypt = UangKuAPI.BusinessObjects.Helper.SecureAES.DataDecrypt(firstDecrypt, _key01);
            return secondDecrypt;
        }
    }
}
