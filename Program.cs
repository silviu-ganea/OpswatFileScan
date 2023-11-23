using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<IFileHashingService, FileHashingService>();
        services.AddTransient<IScanResultFormatter, ScanResultFormatter>();
        services.AddTransient<IFileScannerManager, FileScannerManager>();
        services.AddHttpClient<IFileScannerApiService, OpswatFileScannerApiService>()
            .ConfigureHttpClient(client =>
            {
                string apiKey = context.Configuration["apiKey"]!;
                client.DefaultRequestHeaders.Add("apikey", apiKey);
            });
        
    })
    .Build();

await RunApplicationAsync(host.Services, args);

static async Task RunApplicationAsync(IServiceProvider services, string[] args)
{
    if (args.Length < 1)
    {
        Console.WriteLine("Please provide a file path for the file to be scanned. Usage: dotnet run -- <file-path> [--apiKey=<api-key>]");
        return;
    }

    try
    {
        var filePath = args[0];
        var fileScannerManager = services.GetRequiredService<IFileScannerManager>();
        var scanResultFormatter = services.GetRequiredService<IScanResultFormatter>();

        var scanResult = await fileScannerManager.RunScanAsync(filePath);

        var outputText = scanResultFormatter.Format(scanResult);
        
        Console.Write(outputText);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}

