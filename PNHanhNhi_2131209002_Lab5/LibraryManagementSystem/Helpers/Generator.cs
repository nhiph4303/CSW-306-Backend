namespace LibraryManagementSystem.Helpers
{
    public class Generator
    {
        public static string GenerateCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GenerateUserCode(string prefix = "USER-")
        {
            return prefix + GenerateCode(8);
        }
    }
}
