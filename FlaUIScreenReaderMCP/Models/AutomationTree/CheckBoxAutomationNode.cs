using FlaUI.Core.AutomationElements;


public class CheckBoxAutomationNode : AutomationNode
{
    public CheckBoxAutomationNode(AutomationElement element) : base(element)
    {
        AutomationNodeType = AutomationNodeType.CheckBox;
    }

    public bool? IsChecked { get; set; }
    public required string Text { get; set; }
}