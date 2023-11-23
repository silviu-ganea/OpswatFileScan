using System.Security.Cryptography;

public class FileHashingService : IFileHashingService
{
    public string CalculateMD5(string filePath)
    {
        using (var md5 = MD5.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
            }
        }
    }

    public string CalculateSHA1(string filePath)
    {
        using (var sha1 = SHA1.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                return BitConverter.ToString(sha1.ComputeHash(stream)).Replace("-", "").ToLower();
            }
        }
    }

    public string CalculateSHA256(string filePath)
    {
        using (var sha256 = SHA256.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                return BitConverter.ToString(sha256.ComputeHash(stream)).Replace("-", "").ToLower();
            }
        }
    }
}
