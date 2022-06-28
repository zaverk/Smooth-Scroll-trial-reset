using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace stss
{
    public static class Ssts
    {

        public static void Main()
        {
            foreach (var pr in Process.GetProcessesByName("SmoothScroll"))
            {
                pr.Kill();
            }
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            int dot = 1;
            string user_value = "1";
            try
            {
                dot = Registry.CurrentUser.OpenSubKey(@"Software\SmoothScroll").GetValue("kSSClientId").ToString().IndexOf('.');
                user_value = Registry.CurrentUser.OpenSubKey(@"Software\SmoothScroll").GetValue("kSSClientId").ToString()[..dot];
            }
            catch
            {
                Registry.CurrentUser.CreateSubKey(@"Software\SmoothScroll").SetValue("kSSClientId", user_value + '.' + secondsSinceEpoch);
            }
            Registry.CurrentUser.CreateSubKey(@"Software\SmoothScroll").SetValue("kSSClientId", user_value + '.' + secondsSinceEpoch);
            Registry.CurrentUser.CreateSubKey(@"Software\SmoothScroll").SetValue("kSSInstallDate", secondsSinceEpoch, RegistryValueKind.String);
            Process.Start("SmoothScroll.exe");
        }
    }
}
