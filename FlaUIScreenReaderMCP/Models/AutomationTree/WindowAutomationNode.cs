using FlaUI.Core.AutomationElements;

public class WindowAutomationNode : AutomationNode
{
    public WindowAutomationNode(AutomationElement element) : base(element)
    {
        AutomationNodeType = AutomationNodeType.Window;
    }

    public List<AutomationNode> AutomationNodes { get; set; } = new List<AutomationNode>();
    public required string Title { get; set; }
}