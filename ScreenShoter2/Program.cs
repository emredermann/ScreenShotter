using System;
using System.Windows;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing.Imaging;

public class Program
{

    // int :: how many times did it invoked.
    //
    private static void ScreenShot()
    {
      
        Bitmap printscreen = new Bitmap(SystemInformation.PrimaryMonitorSize.Width * 2, (SystemInformation.PrimaryMonitorSize.Height * 2 ));

        Graphics graphics = Graphics.FromImage(printscreen as Image);
        graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
        printscreen.Save(@"C:\Users\6emre\Desktop\printScreen.jpg", ImageFormat.Jpeg);
    }
   
    private static void Keyevent(KeyPressEventArgs e)
    {
        if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.PrintScreen)))
        {
            ScreenShot();
            e.Handled = true;
        }
    }
    
    
    
    
    static void Main(string[] args)
    {


        if (Clipboard.ContainsImage())
        {
         
            Clipboard.GetImage().Save(@"C:\Users\6emre\Desktop\printScreen.jpg", ImageFormat.Jpeg);
           
        }

    }
}
