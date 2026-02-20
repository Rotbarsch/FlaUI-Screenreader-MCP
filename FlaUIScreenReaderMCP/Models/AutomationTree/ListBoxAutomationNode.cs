using FlaUI.Core.AutomationElements;


public class ListBoxAutomationNode : AutomationNode
{
    public ListBoxAutomationNode(AutomationElement element) : base(element)
    {
        AutomationNodeType = AutomationNodeType.ListBox;
    }

    public string? SelectedItem { get; set; }

    public List<string> Items { get; set; }
}