using System;
using System.Collections.Generic;

using System.Text;


namespace Akant
{
    public class Request
    {
        public string username;
        public string emailVerifier;
        public string email;
        public string phone;
        public string address;
        public string city;
        public string country;
        public string software;
        public string bios;
        public string password;

        public Request()
        {
            username = email = emailVerifier = phone = "default";
            address = city = country = software = bios = password = "default";
        }


    }
}
