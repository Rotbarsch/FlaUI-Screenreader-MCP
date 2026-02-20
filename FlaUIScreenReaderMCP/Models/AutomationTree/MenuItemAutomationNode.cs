using FlaUI.Core.AutomationElements;

namespace FlaUIScreenReaderMCP.Models.AutomationTree;

public class MenuItemAutomationNode : AutomationNode
{
    public MenuItemAutomationNode(AutomationElement element) : base(element)
    {
        AutomationNodeType = AutomationNodeType.MenuItem;
    }

    public List<MenuItemAutomationNode> MenuItems { get; set; } = new List<MenuItemAutomationNode>();
}