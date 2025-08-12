namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Utilities
{
    public interface IUtilityHelper
    {
        string Encrypt(string plainText, string key);

        string Decrypt(string encryptedText, string key);

        string GenerateOtpRef();

        string GenerateOtpCode();

        string ConvertPolicyType(string policyType);

        string GetDescription(Enum valueEnum);

        int ConvertToInteger(string value);

        double ConvertToDouble(string value);

        decimal ConvertToDecimal(string value);
    }
}