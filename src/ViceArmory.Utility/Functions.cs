using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ViceArmory.Utility
{
    public class Functions
    {

        public static visitor GetIpAddress()
        {
            visitor _visitor = new visitor();
            string HostName = Dns.GetHostName();
            Console.WriteLine("Host Name of machine =" + HostName);
            IPAddress[] ipaddress = Dns.GetHostAddresses(HostName);
            foreach (var ip in ipaddress)
            {
                _visitor.Ip += ip;
            }
            _visitor.HostName = HostName;
            return _visitor;
        }
    }
    public class visitor
    {
        
        public string HostName {get; set;}
        public string Ip { get; set; }
    }
}
