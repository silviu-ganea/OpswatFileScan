public interface IFileScannerManager{
    Task<FileScanResult> RunScanAsync(string filePath);
}