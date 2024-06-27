using System.Text;

namespace UI.AutomationChallange.Utils
{
    internal class Generics
    {
        private static Random random = new Random();
        public string GenerateLastNameOnUUID()
        {
            string baseLastName = "test";
            string uniqueId = Guid.NewGuid().ToString().Substring(0, 8);
            string uniqueLastName = baseLastName + "_" + uniqueId;

            return uniqueLastName;
        }

        public string GenerateFirstName()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=[]{}|;:,.<>?";
            StringBuilder stringBuilder = new StringBuilder(5);

            for (int i = 0; i < 5; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }
    }
}
