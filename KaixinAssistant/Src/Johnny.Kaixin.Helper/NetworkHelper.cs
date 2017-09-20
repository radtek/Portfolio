using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Web.Security;

namespace Johnny.Kaixin.Helper
{
    public class NetworkHelper
    {
        [DllImport("wininet.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool InternetGetConnectedState(ref int lpdwFlags, int dwReserved);
        [DllImport("SENSAPI.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern long IsNetworkAlive(ref long lpdwFlags);

        //************************************** 
        // Name: Check internet connection status and LanConnection type 
        // This code makes API calls to check for an internet conection and 
        private const int NETWORK_ALIVE_LAN = 0x1;
        private const int NETWORK_ALIVE_WAN = 0x2;
        //Function to check for the internet connected or not 
        public static bool IsConnected()
        {
            int i = 0;
            //Returns true if there is any internet connection. 
            return InternetGetConnectedState(ref i, 0);
        }
        //Function to check the lan Connection type 
        public static string ConnectionType()
        {
            string functionReturnValue = null;
            long ret = 0;
            if (IsNetworkAlive(ref ret) > 0)
            {
                //If the Network connection is found then it will return a value a greater than zero 
                functionReturnValue = (ret == NETWORK_ALIVE_LAN ? "Connected Via LAN" : "Connected Via WAN");
            }
            else
            {
                functionReturnValue = "Not connected to Lan";
            }
            return functionReturnValue;
        }
        ////Usage' In a Form add a button and in the click event code this 
        //private void Command1_Click(object eventSender, System.EventArgs eventArgs)
        //{
        //    //Private CheckConn As New CheckInternetConnectionType() 
        //    //Prints true if connected to Internet ,False when not connected 
        //    Console.WriteLine(CheckInternetConnectionType.IsConnected());
        //    //Prints Lan or Wan 
        //    Console.WriteLine(CheckInternetConnectionType.ConnectionType());
        //}

        public static bool IsMSNetwork()
        {
            try
            {
                //PRCSGIM00899  004B6636D07AB2464FA44EC7C190A01B
                //FAREAST       7318EB5BCB8F7E681F50F35D26BE8C49
                //v-jozhua      A7991F3152117B471A070A4907D7F8FA
                if (FormsAuthentication.HashPasswordForStoringInConfigFile(System.Environment.MachineName, "MD5") == "004B6636D07AB2464FA44EC7C190A01B" &&
                    FormsAuthentication.HashPasswordForStoringInConfigFile(System.Environment.UserDomainName, "MD5") == "7318EB5BCB8F7E681F50F35D26BE8C49" &&
                    FormsAuthentication.HashPasswordForStoringInConfigFile(System.Environment.UserName, "MD5") == "A7991F3152117B471A070A4907D7F8FA")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool IsMSProxyEnabled()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            if (nics != null && nics.Length == 3)
            {
                foreach (NetworkInterface oNetAdapter in nics)
                {
                    //IT Connection Manager 8D08C6C7B7ED1F71E9A51D72A771DBED
                    if (FormsAuthentication.HashPasswordForStoringInConfigFile(oNetAdapter.Name, "MD5") == "8D08C6C7B7ED1F71E9A51D72A771DBED" && oNetAdapter.NetworkInterfaceType == NetworkInterfaceType.Ppp)
                        return true;
                }
            }
            return false;
        }
    }
}
