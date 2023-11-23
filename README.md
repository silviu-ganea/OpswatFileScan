A tool that can scan a file and output the results in a console, using Opswat Api

How to run:
OpswatFileScan.exe <filepath> --apiKey=<opswat-apikey>
<filepath> can be a file name if the file is in the same location as the executable.

How to build:
Windows 64 bit self-contained executable: dotnet publish -c Release --self-contained true -r win-x64 -p:PublishSingleFile=true 
 
