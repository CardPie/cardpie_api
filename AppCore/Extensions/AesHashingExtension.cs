using System.Security.Cryptography;
using System.Text;

namespace AppCore.Extensions;

public static class EncryptDecrypt
{
    private static readonly string passphrase = "TRUONGGIANGDEPTRAICUBU";
    private static readonly byte[] salt = Encoding.UTF8.GetBytes("NHATTHEGIOI");

    public static string Encrypt(string plainText)
    {
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

        using (Aes aesAlg = Aes.Create())
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(passphrase, salt, 1000);
            byte[] key = pbkdf2.GetBytes(aesAlg.KeySize / 8);
            byte[] iv = aesAlg.IV;

            using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(key, iv))
            using (var msEncrypt = new System.IO.MemoryStream())
            {
                msEncrypt.Write(iv, 0, iv.Length);

                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(plainBytes, 0, plainBytes.Length);
                    csEncrypt.FlushFinalBlock();
                }

                byte[] encryptedBytes = msEncrypt.ToArray();
                string encryptedText = Convert.ToBase64String(encryptedBytes);
                return encryptedText;
            }
        }
    }

    public static string Decrypt(string encryptedText)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

        using (Aes aesAlg = Aes.Create())
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(passphrase, salt, 1000);
            byte[] key = pbkdf2.GetBytes(aesAlg.KeySize / 8);

            byte[] iv = new byte[aesAlg.BlockSize / 8];
            Buffer.BlockCopy(encryptedBytes, 0, iv, 0, iv.Length);

            using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(key, iv))
            using (var msDecrypt = new System.IO.MemoryStream(encryptedBytes, iv.Length, encryptedBytes.Length - iv.Length))
            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
            {
                string plainText = srDecrypt.ReadToEnd();
                return plainText;
            }
        }
    }
}