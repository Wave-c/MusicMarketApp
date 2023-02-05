using System.Security.Cryptography;
using System.Text;

namespace MusicMarketKursach.Helpers
{
    public class Encryptor
    {
        public string GenerateHash(string source)
        {
            var md5 = MD5.Create();

            var bytes = Encoding.ASCII.GetBytes(source);
            var hash = md5.ComputeHash(bytes);

            var sb = new StringBuilder();
            foreach (var b in hash)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
