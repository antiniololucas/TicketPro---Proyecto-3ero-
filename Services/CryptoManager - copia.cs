using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class CryptoManager
    {
        public static string EncryptString(string text)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            StringBuilder sb = new StringBuilder();

            byte[] stream = sha256.ComputeHash(encoding.GetBytes(text));

            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);

            return sb.ToString();
        }

        private static string Key = "01234567890123456789012345678901";

        public static string ReversibleEncrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Generar el vector de inicialización (IV)
                aesAlg.GenerateIV();
                byte[] iv = aesAlg.IV;

                // Configurar la clave
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);

                // Crear un encriptador para realizar la transformación de flujo
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, iv);

                // Crear los streams usados para la encriptación
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // Escribir el IV al inicio del stream
                    msEncrypt.Write(iv, 0, iv.Length);

                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            // Escribir los datos en el CryptoStream
                            swEncrypt.Write(plainText);
                        }
                    }

                    byte[] encrypted = msEncrypt.ToArray();
                    return Convert.ToBase64String(encrypted);
                }
            }
        }

        public static string ReversibleDecrypt(string cipherText)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);

            using (Aes aesAlg = Aes.Create())
            {
                // Extraer el IV del texto cifrado
                byte[] iv = new byte[aesAlg.BlockSize / 8];
                byte[] cipherTextBytes = new byte[fullCipher.Length - iv.Length];

                Array.Copy(fullCipher, iv, iv.Length);
                Array.Copy(fullCipher, iv.Length, cipherTextBytes, 0, cipherTextBytes.Length);

                // Configurar la clave
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = iv;

                // Crear un desencriptador para realizar la transformación de flujo
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Crear los streams usados para la desencriptación
                using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Leer los datos desencriptados del CryptoStream
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
