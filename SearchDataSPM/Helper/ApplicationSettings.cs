using SPMConnectAPI;
using System.Collections.Generic;
using System.Linq;

namespace SearchDataSPM.Helper
{
    public static class ApplicationSettings
    {
        /// <summary>
        /// This application uses project files of this version.
        /// </summary>
        public static string UserOpenItemLetter = "A";

        /// <summary>
        /// This application uses project files of this version.
        /// </summary>
        public static List<ConnectParameters> connectParameters;

        /// <summary>
        /// This field monitors the screen shot location
        /// </summary>
        public static string ScreenshotLocation = "";

        /// <summary>
        /// Determines which settings to save.
        /// </summary>
        public enum SaveSettings
        {
            /// <summary>Save all settings.</summary>
            All = 0,

            /// <summary>Save only the settings which are modified via the Settings form.</summary>
            UserModifiable,
        }

        /// <summary>
        /// Retrieve the application settings.
        /// </summary>
        public static void Load()
        {
            SPMConnectAPI.ConnectAPI connectapi = new SPMConnectAPI.ConnectAPI();
            connectParameters = connectapi.GetCustomObjects();
        }

        /// <summary>
        /// Save the application settings.
        /// </summary>
        public static void Save(SaveSettings settingsSelection = SaveSettings.All)
        {
            // User modifiable settings
            if ((settingsSelection == SaveSettings.All) || (settingsSelection == SaveSettings.UserModifiable))
            {
            }

            Properties.Settings.Default.Save();
        }

        public static string GetConnectParameterValue(string para)
        {
            return connectParameters.First(s => s.Parameter == para).ParameterValue ?? "";
        }

        /// <summary>
        /// Sets application default values for the properties that can be changed by the user.
        /// </summary>
        public static void SetDefaultValues()
        {
            UserOpenItemLetter = "A";
            ScreenshotLocation = "";
        }
    }
}