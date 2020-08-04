using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDataSPM.Helper
{
    public static class ApplicationSettings
    {
        /// <summary>
        /// This application uses project files of this version.
        /// </summary>
        public static string UserOpenItemLetter = "A";

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
