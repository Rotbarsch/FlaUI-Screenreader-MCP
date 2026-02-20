using FlaUI.Core.AutomationElements;


public class DataGridAutomationNode : AutomationNode
{
    public DataGridAutomationNode(AutomationElement element) : base(element)
    {
        AutomationNodeType = AutomationNodeType.DataGrid;
    }

    public List<string> Columns { get; set; }
}