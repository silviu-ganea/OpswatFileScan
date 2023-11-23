using System.Text;

public class ScanResultFormatter : IScanResultFormatter
{
    public string Format(FileScanResult scanResult)
    {
        if (scanResult.IsSuccess && scanResult.ScanResult != null)
        {
            return FormatReport(scanResult.ScanResult);
        }
        else
        {
            return $"Error: {scanResult.ErrorMessage}";
        }
    }

    private string FormatReport(ScanResultDto response)
    {
        var builder = new StringBuilder();
        builder.AppendLine($"------------File scan results------------");
        builder.AppendLine($"Filename: {response.FileInfo.DisplayName}");
        builder.AppendLine($"OverallStatus: {response.ProcessInfo.Result}");

        foreach (var detail in response.ScanResults.ScanDetails)
        {
            builder.AppendLine($"Engine: {detail.Key}");
            builder.AppendLine($"ThreatFound: {GetThreatFound(detail.Value.ScanResultI)}");
            builder.AppendLine($"ScanResult: {detail.Value.ScanResultI}");
            builder.AppendLine($"DefTime: {detail.Value.DefTime}");
            builder.AppendLine();
        }

        builder.AppendLine("END.");

        return builder.ToString();
    }

    private static string GetThreatFound(int scanResult)
    {
        return scanResult == 0 ? "Clean" : "SomeBadMalwareWeFound";
    }
}
