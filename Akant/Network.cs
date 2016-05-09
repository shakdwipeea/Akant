using System;
using System.Net;
using System.Collections.Generic;

using System.Text;

using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace Akant
{
    public class Network
    {
       public static string sendData(string uname,string password,string bios)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://54.191.165.127:8000/softwareName");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";
            var result = "Hello";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                

                string json = "{\"username\":\"" + uname + "\",\"password\":\"" + password + "\",\"bios\":\"" + bios +"\"}";
                    


                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                    return result;
                }
                
            }

        }

       public static string sendData(Request req)
       {
           var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://54.191.165.127:8000/register");
           httpWebRequest.ContentType = "text/json";
           httpWebRequest.Method = "POST";
           var result = "Hello";

           using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
           {


               //string json = "{\"username\":\"" + uname + "\",\"password\":\"" + password + "\",\"bios\":\"" + bios + "\"}";
               string json = JsonConvert.SerializeObject(req);


               streamWriter.Write(json);
               streamWriter.Flush();
               streamWriter.Close();

               var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();


               using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
               {
                   result = streamReader.ReadToEnd();
                   return result;
               }

           }
       }
    }
}
