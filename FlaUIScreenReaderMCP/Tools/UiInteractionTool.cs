using System.ComponentModel;
using FlaUIScreenReaderMCP.Models.MCP;
using FlaUIScreenReaderMCP.Services;
using ModelContextProtocol.Server;

namespace FlaUIScreenReaderMCP.Tools;

[McpServerToolType]
[Description("Provides tools for interacting with UI elements.")]
public static class UiInteractionTool
{
    [McpServerTool(Name = "highlight_control", Title = "Highlight a control")]
    [Description("Highlights an element visually in red for 30 seconds.")]
    public static string HighlightElement(
        [Description("Name of the process, under which the element resides.")]
        string processName,
        [Description("AutomationId or Name of the element to highlight.")]
        string automationIdOrName)
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

    [McpServerTool(Name = "click_control", Title = "Click a control")]
    [Description("Performs a click on an element.")]
    public static string ClickElement(
        [Description("Name of the process, under which the element resides.")]
        string processName,
        [Description("AutomationId or Name of the element to click.")]
        string automationIdOrName)
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

    [McpServerTool(Name = "select_item", Title = "Select an item in a control")]
    [Description("Selects an item in an element (for example, ListBox or ComboBox controls).")]
    public static string SelectItemInElement(
        [Description("Name of the process, under which the element resides.")]
        string processName,
        [Description("AutomationId or Name of the element.")]
        string automationIdOrName,
        [Description("Item to select.")]
        string itemText)
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