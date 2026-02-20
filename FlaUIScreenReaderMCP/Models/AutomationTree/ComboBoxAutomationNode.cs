using FlaUI.Core.AutomationElements;


public class ComboBoxAutomationNode : AutomationNode
{
    public ComboBoxAutomationNode(AutomationElement element) : base(element)
    {
        AutomationNodeType = AutomationNodeType.ComboBox;
    }

    public string? SelectedItem { get; set; }
    public List<string> Items { get; set; } = new List<string>();
}