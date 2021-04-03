using System.Security.Cryptography;
using System.Text;
using Common.Extensions;

namespace Common
{
    public static class Md5
    {
        public static string HashToHexadecimal(string input)
        {
            using var md5 = MD5.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = md5.ComputeHash(bytes);
            var sb = new StringBuilder();
            hashBytes.ForEach(b => sb.Append(b.ToString("X2")));
            return sb.ToString();
        }
    }
}
