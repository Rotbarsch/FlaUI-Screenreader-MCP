using System.ComponentModel;
using FlaUIScreenReaderMCP.Models.MCP;
using FlaUIScreenReaderMCP.Services;
using ModelContextProtocol.Server;

namespace FlaUIScreenReaderMCP.Tools;

[McpServerToolType]
public static class ScreenReaderTool
{
    [McpServerTool, Description("Returns information about the UIA3 automation tree of the selected process. Accepts the name of the process (without .exe!) as parameter.")]
    public static string GetAutomationTreeOfProcess(string processName)
    {
        if (!ScreenReader.TryGetScreenReaderForProcess(processName, out var sr))
        {
            return JsonResponseHelper.ToJsonResponse(new McpFailureResponse
            {
                Error = $"Unable to create ScreenReader for process '{processName}'. Make sure the process is running."
            });
        }

        return JsonResponseHelper.ToJsonResponse(sr!.GetAutomationTree());
    }
}