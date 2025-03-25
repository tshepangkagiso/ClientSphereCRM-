namespace CRM_API.Services
{
    public class PasswordServices
    {
        private const int SaltRounds = 12;

        public static string EncryptPassword(string password)
        {
            try
            {
                return BCrypt.Net.BCrypt.HashPassword(password,SaltRounds);
            }
            catch
            {
                throw new Exception("Error occured with encryption.");
            }
        }

        public static bool IsVerifiedPassword(string password, string hashedPassword)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password,hashedPassword);
            }
            catch
            {
                throw new Exception("Error occured with encryption.");
            }
        }
    }
}
