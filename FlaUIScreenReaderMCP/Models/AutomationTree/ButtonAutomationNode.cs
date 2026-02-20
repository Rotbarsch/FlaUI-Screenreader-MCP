using FlaUI.Core.AutomationElements;


public class ButtonAutomationNode : AutomationNode
{
    public ButtonAutomationNode(AutomationElement element):base(element)
    {
        AutomationNodeType = AutomationNodeType.Button;
    }

    public string? HelpText { get; set; }
    public bool Enabled { get; set; }
}