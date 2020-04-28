using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMConnectAPI
{
    /// <summary>
    /// Helper method class to call quick static methods
    /// </summary>
    public static class Helper
    {
        public static string GetUserName()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if (userName.Length > 0)
            {
                return userName;
            }
            else
            {
                return null;
            }
        }
    }
}
