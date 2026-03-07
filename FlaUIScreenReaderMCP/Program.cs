using FlaUIScreenReaderMCP.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

if (Environment.OSVersion.Platform != PlatformID.Win32NT)
{
    Console.Error.WriteLine("This application is only supported on Windows.");
}

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<ScreenReaderTool>()
    .WithTools<UiInteractionTool>();

await builder.Build().RunAsync();