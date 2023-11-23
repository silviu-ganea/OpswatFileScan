public interface IFileHashingService
{
    string CalculateMD5(string filePath);
    string CalculateSHA1(string filePath);
    string CalculateSHA256(string filePath);
}
