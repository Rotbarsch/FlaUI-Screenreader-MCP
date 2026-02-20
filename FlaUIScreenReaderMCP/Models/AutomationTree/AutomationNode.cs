using FlaUI.Core.AutomationElements;

public abstract class AutomationNode(AutomationElement automationElement)
{
    public string AutomationId { get; set; } = automationElement.AutomationId;
    public string Name { get; set; } = automationElement.Name;
    public AutomationNodeType AutomationNodeType { get; set; }
    public BoundingRectangle BoundingRectangle { get; set; } = new()
    {
        Width = automationElement.BoundingRectangle.Width,
        Height = automationElement.BoundingRectangle.Height,
        OriginX = automationElement.BoundingRectangle.X,
        OriginY = automationElement.BoundingRectangle.Y,
    };
}