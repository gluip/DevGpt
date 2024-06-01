using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevGpt.Models.OpenAI;

namespace DevGpt.Commands.Windows;

[StructLayout(LayoutKind.Sequential)]
struct CURSORINFO
{
    public Int32 cbSize;
    public Int32 flags;
    public IntPtr hCursor;
    public POINTAPI ptScreenPos;
}

[StructLayout(LayoutKind.Sequential)]
struct POINTAPI
{
    public int x;
    public int y;
}



public class WindowsCommandBase
{

    [DllImport("user32.dll")]
    static extern bool GetCursorInfo(out CURSORINFO pci);

    [DllImport("user32.dll")]
    static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);

    const Int32 CURSOR_SHOWING = 0x00000001;


    protected IList<DevGptChatMessage> ScreenshotMessage(DevGptToolCallResultMessage userMessage,bool includeScreenshot = true)
    {
        var htmlContextMessage = new DevGptContextMessage("screenshot",
            new List<DevGptContent>()
            {
                new DevGptContent(DevGptContentType.Text, "This is a screenshot"),
                new DevGptContent(DevGptContentType.ImageUrl, GetDataImagePngBase64())
            });

        var devGptChatMessages = new List<DevGptChatMessage>
        {
            userMessage
        };
        if (includeScreenshot)
        {
            devGptChatMessages.Add(htmlContextMessage);
        }
        return devGptChatMessages;
    }

    private string GetDataImagePngBase64()
    {
        var bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        using (var g = Graphics.FromImage(bmp))
        {
            g.CopyFromScreen(0, 0, 0, 0, bmp.Size);
            //draw the cursor as a mouse icon
            //var cursor = new Cursor(Cursor.Current.Handle);

            CURSORINFO pci;
            pci.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CURSORINFO));

            if (GetCursorInfo(out pci))
            {

                if (pci.flags == CURSOR_SHOWING)
                {
                    
                    DrawIcon(g.GetHdc(), pci.ptScreenPos.x, pci.ptScreenPos.y, pci.hCursor);
                    g.ReleaseHdc();
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            // draw a red rectangle around the cursor
            //g.DrawRectangle(new Pen(Color.Red, 4), pci.ptScreenPos.x-10, pci.ptScreenPos.y-10, 40, 40);
        }

        //resize the image to 50% of the original size
        var newWidth = bmp.Width / 2;
        var newHeight = bmp.Height / 2;
        var newBmp = new Bitmap(bmp, newWidth, newHeight);
        bmp.Dispose();

      


        using (var ms = new MemoryStream())
        {
            newBmp.Save(ms, ImageFormat.Png);

            var filename = $"C:\\devgpt\\logs\\screenshots\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

            newBmp.Save(filename);
            var data = Convert.ToBase64String(ms.ToArray());
            //convert to base64 Uri
            var dataImagePngBase64 = "data:image/png;base64," + data;
            //add a timestamp to screenshot
            return dataImagePngBase64;
        }
    }
}