using System.Diagnostics;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using FlaUIScreenReaderMCP.Models.AutomationTree;

namespace FlaUIScreenReaderMCP.Services;

internal class ScreenReader
{
    private readonly string _processName;

    public static bool TryGetScreenReaderForProcess(string processName, out ScreenReader? screenReader)
    {
        var process = Process.GetProcessesByName(processName).FirstOrDefault();
        if (process == null)
        {
            screenReader = null;
            return false;
        }
        screenReader = new ScreenReader(processName);
        return true;
    }

    private ScreenReader(string processName)
    {
        _processName = processName;
    }

    public List<WindowAutomationNode> GetAutomationTree()
    {
        var process = Process.GetProcessesByName(_processName).First();

        var app = Application.Attach(process);
        var windows = app.GetAllTopLevelWindows(new UIA3Automation());
        var aeWindows = new List<WindowAutomationNode>();

        foreach (var window in windows)
        {
            aeWindows.Add(HandleWindow(window));
        }

        return aeWindows;
    }

    private WindowAutomationNode HandleWindow(Window window)
    {
        var desc = window.FindAllDescendants(cf =>
                cf.ByControlType(ControlType.Menu)
                    .Or(cf.ByControlType(ControlType.CheckBox))
                    .Or(cf.ByControlType(ControlType.Button))
                    .Or(cf.ByControlType(ControlType.Edit))
                    .Or(cf.ByControlType(ControlType.ComboBox))
                    .Or(cf.ByControlType(ControlType.List))
                    .Or(cf.ByControlType(ControlType.DataGrid))
                    .Or(cf.ByControlType(ControlType.Window))
                    .Or(cf.ByControlType(ControlType.Text))
            )

            .Where(x => x.FrameworkType is FrameworkType.Wpf or FrameworkType.WinForms or FrameworkType.Win32)
            .Where(ae => GetContainingWindow(ae).Title == window.Title)
            .ToList();

        var aeWindow = new WindowAutomationNode(window)
        {
            Title = window.Title
        };

        var items = new List<AutomationNode>();
        foreach (var item in desc)
        {
            var node = item.ControlType switch
            {
                ControlType.Window    => HandleWindow(item.AsWindow()),
                ControlType.Menu      => HandleMenuBar(item.AsMenu()),
                ControlType.CheckBox  => HandleCheckbox(item.AsCheckBox()),
                ControlType.Button    => HandleButton(item.AsButton()),
                ControlType.Edit      => HandleEdit(item.AsTextBox()),
                ControlType.ComboBox  => HandleComboBox(item.AsComboBox()),
                ControlType.List      => HandleListBox(item.AsListBox()),
                ControlType.Text      => HandleText(item.AsLabel()),
                ControlType.DataGrid  => HandleDataGrid(item.AsDataGridView()),
                _ => null
            };
            if (node != null)
                items.Add(node);
        }

        aeWindow.AutomationNodes = items;
        return aeWindow;
    }
    
    private Window GetContainingWindow(AutomationElement ae)
    {
        var parent = ae.Parent;
        while (parent.ControlType is not ControlType.Window)
        {
            parent = parent.Parent;
        }

        return parent.AsWindow();
    }

    private AutomationNode HandleDataGrid(DataGridView asDataGridView)
    {
        return new DataGridAutomationNode(asDataGridView)
        {
            Columns = asDataGridView.Header?.Columns.Select(x => x.Text).ToList() ?? []
        };
    }

    private AutomationNode HandleListBox(ListBox asListBox)
    {
        return new ListBoxAutomationNode(asListBox)
        {
            SelectedItem = asListBox.Patterns.SelectionItem.IsSupported ? asListBox.SelectedItem?.Text : null,
            Items = asListBox.Items.Select(x => x.Text).ToList()
        };
    }

    private AutomationNode HandleComboBox(ComboBox asComboBox)
    {
        return new ComboBoxAutomationNode(asComboBox)
        {
            SelectedItem = asComboBox.SelectedItem?.Text,
            Items = asComboBox.Items.Select(x => x.Text).ToList()
        };
    }

    private AutomationNode HandleEdit(TextBox asTextBox)
    {
        return new TextBoxAutomationNode(asTextBox)
        {
            Text = asTextBox.Text,
        };
    }

    private AutomationNode HandleButton(Button asButton)
    {
        return new ButtonAutomationNode(asButton)
        {
            HelpText = asButton.HelpText,
            Enabled = asButton.IsEnabled,
        };
    }

    private AutomationNode HandleCheckbox(CheckBox asCheckBox)
    {
        return new CheckBoxAutomationNode(asCheckBox)
        {
            IsChecked = asCheckBox.IsChecked,
            Text = asCheckBox.Text,
        };
    }

    private MenuAutomationNode HandleMenuBar(Menu asMenu)
    {
        var result = new MenuAutomationNode(asMenu)
        {
            MenuItems = asMenu.Items.Select(i => HandleMenuItem(i.AsMenuItem()))
                .ToList()
        };
        return result;
    }

    private MenuItemAutomationNode HandleMenuItem(MenuItem asMenuItem)
    {
        return new MenuItemAutomationNode(asMenuItem)
        {
            MenuItems = asMenuItem.Items.Select(x => HandleMenuItem(x.AsMenuItem())).ToList()
        };
    }


    private AutomationNode? HandleText(Label asLabel)
    {
        //Filter out Labels which are part of any of the currently tracked controls. We're only interested in "free-floating" labels.
        if (asLabel.Parent.ControlType is ControlType.MenuItem) return null;
        if (asLabel.Parent.ControlType is ControlType.HeaderItem) return null;

        return new LabelAutomationNode(asLabel)
        {
            Text = asLabel.Text,
        };
    }
}