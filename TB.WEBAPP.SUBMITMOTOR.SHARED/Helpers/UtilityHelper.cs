using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Utilities;
using TB.WEBAPP.SUBMITMOTOR.DOMIAN.Enums;

namespace TB.WEBAPP.SUBMITMOTOR.SHARED.Helpers
{
    public class UtilityHelper : IUtilityHelper
    {
        public string Encrypt(string plainText, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.PadRight(32)); // Key ต้อง 32 bytes สำหรับ AES-256
            using Aes aes = Aes.Create();
            aes.Key = keyBytes;
            aes.GenerateIV();
            using var encryptor = aes.CreateEncryptor();
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encrypted = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            // รวม IV กับ ciphertext แล้วแปลงเป็น Base64
            byte[] result = aes.IV.Concat(encrypted).ToArray();
            return Convert.ToBase64String(result);
        }

        public string Decrypt(string encryptedText, string key)
        {
            byte[] fullCipher = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.PadRight(32)); // ใช้ key เดิม

            using Aes aes = Aes.Create();
            aes.Key = keyBytes;
            byte[] iv = [.. fullCipher.Take(16)];
            byte[] cipherText = [.. fullCipher.Skip(16)];
            aes.IV = iv;

            using var descriptor = aes.CreateDecryptor();
            byte[] decrypted = descriptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
            return Encoding.UTF8.GetString(decrypted);
        }

        public string GenerateOtpRef()
        {
            var chars1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            var stringChars1 = new char[6];
            var random1 = new Random();

            for (int i = 0; i < stringChars1.Length; i++)
            {
                stringChars1[i] = chars1[random1.Next(chars1.Length)];
            }

            var str = new String(stringChars1);
            return str;
        }

        public string GenerateOtpCode()
        {
            var chars1 = "1234567890";
            var stringChars1 = new char[6];
            var random1 = new Random();

            for (int i = 0; i < stringChars1.Length; i++)
            {
                stringChars1[i] = chars1[random1.Next(chars1.Length)];
            }

            var str = new String(stringChars1);
            return str;
        }

        public string ConvertPolicyType(string policyType)
        {
            var enumValue = policyType switch
            {
                "1" or "1.1" or "1.2" or "1.3" => PolicyType.type1,
                "2" or "2.1" => PolicyType.type2,
                "3" or "3.1" => PolicyType.type3,
                _ => throw new ArgumentException($"Invalid policy type: {policyType}")
            };

            return GetDescription(enumValue);
        }

        public string GetDescription(Enum valueEnum)
        {
            var field = valueEnum.GetType().GetField(valueEnum.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field!, typeof(DescriptionAttribute))!;
            return attribute == null ? valueEnum.ToString() : attribute.Description;
        }

        public int ConvertToInteger(string value)
        {
            var cleanedValue = string.IsNullOrWhiteSpace(value) ? "0" : value.Replace(",", "");

            return int.TryParse(cleanedValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result)
                ? result
                : 0;
        }

        public double ConvertToDouble(string value)
        {
            var cleanedValue = string.IsNullOrWhiteSpace(value) ? "0.00" : value.Replace(",", "");

            return double.TryParse(cleanedValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double result)
                ? result
                : 0.0;
        }

        public decimal ConvertToDecimal(string value)
        {
            var cleanedValue = string.IsNullOrWhiteSpace(value) ? "0.00" : value.Replace(",", "");

            return decimal.TryParse(cleanedValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal result)
                ? result
                : 0.00m;
        }

    }
}
