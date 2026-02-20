using System.Diagnostics;
using System.Drawing;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;

namespace FlaUIScreenReaderMCP.Services;

internal class AutomationElementInteractor
{
    private readonly Application _app;

    private AutomationElementInteractor(string processName)
    {
        var process = Process.GetProcessesByName(processName).First();
        _app = Application.Attach(process);
    }

    public static bool TryGetInteractorForProcess(string processName, out AutomationElementInteractor? interactor)
    {
        var process = Process.GetProcessesByName(processName).FirstOrDefault();
        if (process == null)
        {
            interactor = null;
            return false;
        }

        interactor = new AutomationElementInteractor(processName);
        return true;
    }

    public bool DrawHighlight(string automationIdOrName)
    {
        var element = GetElementByAutomationIdOrName(automationIdOrName);
        if (element is null) return false;
        element.DrawHighlight(false, Color.Red, TimeSpan.FromSeconds(30));
        return true;
    }

    public bool Click(string automationIdOrName)
    {
        var element = GetElementByAutomationIdOrName(automationIdOrName);
        if (element is null) return false;
        
        element.Click();
        return true;
    }

    public bool Select(string automationIdOrName, string item)
    {
        var element = GetElementByAutomationIdOrName(automationIdOrName);
        if(element is null) return false;

        try
        {
            var selected = element.AsComboBox().Select(item);
            return selected is not null;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e.Message);
        }

        return true;
    }

    private AutomationElement? GetElementByAutomationIdOrName(string automationIdOrName)
    {
        return _app.GetAllTopLevelWindows(new UIA3Automation()).Select(x =>
                x.FindFirstDescendant(cf => cf.ByAutomationId(automationIdOrName).Or(cf.ByName(automationIdOrName))))
            .FirstOrDefault();
    }
}