using System.ComponentModel;
using FlaUIScreenReaderMCP.Models.MCP;
using FlaUIScreenReaderMCP.Services;
using ModelContextProtocol.Server;

namespace FlaUIScreenReaderMCP.Tools;

[McpServerToolType]
[Description("Returns the AutomationTree of all windows belonging to a process.")]
public static class ScreenReaderTool
{
    [McpServerTool(Name = "get_automation_tree", Title = "Get Automation Tree of a process")]
    [Description("Returns information about the UIA3 automation tree of the selected process's child windows.")]
    public static string GetAutomationTreeOfProcess(
        [Description("The name of the process (without file ending) to inspect.")]
        string processName)
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