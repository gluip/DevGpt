using System.Runtime.InteropServices;
using DevGpt.Models.Commands;

namespace DevGpt.Commands.Windows;

public class ClickMouseCommand : ICommand
{
    public string[] Arguments => new[] { "button" };
    public string Description => "Clicks the mouse button";
    public string Name => "ClickMouse";

    public string Execute(string[] args)
    {
        if (args.Length != 1)
        {
            throw new ArgumentException("Invalid number of arguments");
        }
        return "clicked";

        var button = args[0].ToLower();
        if (button == "left")
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            return "Left mouse button clicked";
        }
        else if (button == "right")
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            return "Right mouse button clicked";
        }
        else
        {
            throw new ArgumentException("Invalid button argument");
        }
    }

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    private const int MOUSEEVENTF_LEFTDOWN = 0x02;
    private const int MOUSEEVENTF_LEFTUP = 0x04;
    private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const int MOUSEEVENTF_RIGHTUP = 0x10;
    
}