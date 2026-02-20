using System.ComponentModel;
using FlaUIScreenReaderMCP.Models.MCP;
using FlaUIScreenReaderMCP.Services;
using ModelContextProtocol.Server;

namespace FlaUIScreenReaderMCP.Tools;

[McpServerToolType]
public static class UiInteractionTool
{
    [McpServerTool, Description("Highlights the element identified by the provided AutomationId or Name belonging to the provided process in red for 30 seconds.")]
    public static string HighlightElement(string processName, string automationIdOrName)
    {
        if (!AutomationElementInteractor.TryGetInteractorForProcess(processName, out var interactor))
        {
            return JsonResponseHelper.ToJsonResponse(new McpFailureResponse
            {
                Error = $"Unable to create ScreenReader for process '{processName}'. Make sure the process is running."
            });
        }

        return interactor!.DrawHighlight(automationIdOrName)
            ? JsonResponseHelper.ToJsonResponse(new McpSuccessResponse
            {
                Message = "Highlighted the element successfully."
            })
            : JsonResponseHelper.ToJsonResponse(new McpFailureResponse
            {
                Error = "Unable to highlight element. Make sure the element can be accessed by the provided AutomationId or Name."
            });
    }

    [McpServerTool, Description("Clicks the element identified by the provided AutomationId or Name belonging to the provided process in red for 30 seconds.")]
    public static string ClickElement(string processName, string automationIdOrName)
    {
        if (!AutomationElementInteractor.TryGetInteractorForProcess(processName, out var interactor))
        {
            return JsonResponseHelper.ToJsonResponse(new McpFailureResponse
            {
                Error = $"Unable to create ScreenReader for process '{processName}'. Make sure the process is running."
            });
        }
        return interactor!.Click(automationIdOrName)
            ? JsonResponseHelper.ToJsonResponse(new McpSuccessResponse
            {
                Message = "Clicked the element successfully."
            })
            : JsonResponseHelper.ToJsonResponse(new McpFailureResponse
            {
                Error = "Unable to click element. Make sure the element can be accessed by the provided AutomationId or Name and is clickable."
            });
    }

    [McpServerTool, Description("Selects an item (identified by its text) in a combobox identified by AutomationId or Name, which belongs to the provided process.")]
    public static string SelectItemInElement(string processName, string automationIdOrName, string itemText)
    {
        if (!AutomationElementInteractor.TryGetInteractorForProcess(processName, out var interactor))
        {
            return JsonResponseHelper.ToJsonResponse(new McpFailureResponse
            {
                Error = $"Unable to create ScreenReader for process '{processName}'. Make sure the process is running."
            });
        }
        return interactor!.Select(automationIdOrName, itemText)
            ? JsonResponseHelper.ToJsonResponse(new McpSuccessResponse
            {
                Message = "Selected item in the element successfully."
            })
            : JsonResponseHelper.ToJsonResponse(new McpFailureResponse
            {
                Error = "Unable to select item in element. Make sure the element can be accessed by the provided AutomationId or Name and is clickable."
            });
    }
}