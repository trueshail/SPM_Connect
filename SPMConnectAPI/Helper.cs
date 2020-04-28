
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

        public static string ConnectConnectionString()
        {
            return "Data Source=spm-sql;Initial Catalog=SPM_Database;User ID=SPM_Agent;password=spm5445";
        }


        public static string ConnectCntrlsConnectionString()
        {
            return "Data Source=spm-sql;Initial Catalog=SPMControlCatalog;User ID=SPM_Controls;password=eyBzJehFP*uO";
        }
    }
}
