using System;
using System.Collections.Generic;
using System.Text;


namespace Akant
{
   public  class Bios
    {
        public static String getSerialNumber()
        {
            System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c wmic bios get serialnumber");
            procInfo.RedirectStandardOutput = true;
            procInfo.UseShellExecute = false;
            procInfo.CreateNoWindow = true;

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procInfo;
            proc.Start();

            string result = proc.StandardOutput.ReadToEnd();
            result = result.Substring(13, result.Length - 13);
            result = result.Trim();
            return result;
        }
    }
}
