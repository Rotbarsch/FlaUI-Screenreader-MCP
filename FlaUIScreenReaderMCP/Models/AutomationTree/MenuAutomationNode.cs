using FlaUI.Core.AutomationElements;
using FlaUIScreenReaderMCP.Models.AutomationTree;


public class MenuAutomationNode : AutomationNode
{
    public MenuAutomationNode(AutomationElement element) : base(element)
    {
        AutomationNodeType = AutomationNodeType.Menu;
    }

    public List<MenuItemAutomationNode> MenuItems { get; set; } = new List<MenuItemAutomationNode>();
}