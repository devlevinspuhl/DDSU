using Microsoft.Win32;
using System.Drawing;

RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\WinRegistry\ddsu");
key.SetValue("Width", 300);
key.SetValue("Height", 200);
key.Close();

key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\WinRegistry\ddsu");
if (key != null)
{
    int width = int.Parse(key.GetValue("Width").ToString());
    int height = int.Parse(key.GetValue("Height").ToString());
    
}
