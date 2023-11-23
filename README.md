# Opswat File Scan Tool

A tool that can scan a file and output the results in a console, using the Opswat API.

## How to Run

To use the Opswat File Scan Tool, run the executable from the command line with the following syntax:

```plaintext
OpswatFileScan.exe <filepath> --apiKey=<opswat-apikey>
```

- `<filepath>`: The path to the file you want to scan. This can be just the file name if the file is in the same location as the executable.
- `<opswat-apikey>`: Your API key for accessing the Opswat API.

Example:

```plaintext
OpswatFileScan.exe myfile.txt --apiKey=123456789abcdef
```

## How to Build

To build a self-contained executable for Windows 64-bit, use the following command:

```plaintext
dotnet publish -c Release --self-contained true -r win-x64 -p:PublishSingleFile=true
```

This command compiles the application and packages it as a single executable file for Windows 64-bit systems.
