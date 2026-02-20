using FlaUI.Core.AutomationElements;

public class TextBoxAutomationNode : AutomationNode
{
    public TextBoxAutomationNode(AutomationElement element) : base(element)
    {
        AutomationNodeType = AutomationNodeType.TextBox;
    }

    public required string Text { get; set; }
}