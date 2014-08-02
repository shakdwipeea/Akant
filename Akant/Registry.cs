using System;
using System.Collections.Generic;

using System.Text;


namespace Akant
{
   public class Registry
    {
        public static void  createRegistry (string b,string software) {
           Microsoft.Win32.RegistryKey key,key1;
           key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("vr");
           key1 = key.CreateSubKey(software);
           key1.SetValue("Junk", b);
           key.Close();
       }

        

    }
}
