using FlaUI.Core.AutomationElements;

namespace FlaUIScreenReaderMCP.Models.AutomationTree;

public class LabelAutomationNode : AutomationNode
{
    public LabelAutomationNode(AutomationElement element):base(element)
    {
        AutomationNodeType = AutomationNodeType.Label;
    }

    public string? Text { get; set; }
}