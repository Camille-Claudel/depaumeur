using System;
using System.Collections.Generic;
using System.Text;

namespace Depaumer.Utils
{
    public static class MacAddressParser
    {
        private static readonly string usualPrefix = "34:8a:12:c"; // We should put this in the settings, but we really don't have the time right now

        /// <summary>
        /// Returns True if this mac address can be used to identify a position
        /// </summary>
        /// <returns></returns>
        public static bool IsMacAddressValid(string macAddress)
        {
            return macAddress.StartsWith(usualPrefix);
        }

        /// <summary>
        /// Returns a 6 character long version containing only relevant information, doesn't check if the macaddress is valid.
        /// </summary>
        /// <param name="macAddress"></param>
        /// <returns></returns>
        public static string GetStrippedMacAddress(string macAddress)
        {
            return macAddress.Substring(usualPrefix.Length, 6);
        }

    }
}
