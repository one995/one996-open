// See https://aka.ms/new-console-template for more information
using Like.Common.Common;
using Like.TCPServer.TCP;
using Serilog;

 new Appsettings(AppContext.BaseDirectory);
var ip= Appsettings.GetTCPIP();
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs\\\\log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

Console.WriteLine("Hello, World!");
var port = Appsettings.GetTCPPort();
Serilog.Log.Information(port);
new TCPServerHP(int.Parse(port));
Console.ReadKey();