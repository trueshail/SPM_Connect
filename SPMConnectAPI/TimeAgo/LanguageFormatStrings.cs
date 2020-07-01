using System;

namespace SPMConnectAPI
{
    internal static class LanguageFormatStrings
    {
        [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
        internal class VerifiedByNativeSpeakerAttribute : Attribute { }

        private static DateTimeFormatStrings english;

        [VerifiedByNativeSpeaker]
        public static DateTimeFormatStrings English
        {
            get
            {
                return english ?? (english = new DateTimeFormatStrings
                {
                    SecondAgo = "{0} second ago",
                    SecondsAgo = "{0} seconds ago",
                    MinuteAgo = "{0} minute ago",
                    MinutesAgo = "{0} minutes ago",
                    HourAgo = "{0} hour ago",
                    HoursAgo = "{0} hours ago",
                    DayAgo = "{0} day ago",
                    DaysAgo = "{0} days ago",
                    MonthAgo = "{0} month ago",
                    MonthsAgo = "{0} months ago",
                    YearAgo = "{0} year ago",
                    YearsAgo = "{0} years ago",
                });
            }
        }
    }
}