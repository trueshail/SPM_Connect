using System;

namespace SPMConnectAPI
{
    public static class DateTimeExtensions
    {
        public static string TimeAgo(this DateTime dateTime)
        {
            return dateTime.TimeAgo(DateTime.Now);
        }

        public static string TimeAgo(this DateTime dateTime, DateTime relativeTo)
        {
            var timeSpan = relativeTo - dateTime;
            var dateTimeFormatStrings = LanguageFormatStrings.English;
            if (timeSpan.Days > 365)
            {
                var years = (timeSpan.Days / 365);
                if (timeSpan.Days % 365 != 0)
                    years++;
                return string.Format(years == 1 ? dateTimeFormatStrings.YearAgo : dateTimeFormatStrings.YearsAgo, years);
            }
            if (timeSpan.Days > 30)
            {
                var months = (timeSpan.Days / 30);
                if (timeSpan.Days % 31 != 0)
                    months++;
                return string.Format(months == 1 ? dateTimeFormatStrings.MonthAgo : dateTimeFormatStrings.MonthsAgo, months);
            }
            if (timeSpan.Days > 0)
                return string.Format(timeSpan.Days == 1 ? dateTimeFormatStrings.DayAgo : dateTimeFormatStrings.DaysAgo, timeSpan.Days);
            if (timeSpan.Hours > 0)
                return string.Format(timeSpan.Hours == 1 ? dateTimeFormatStrings.HourAgo : dateTimeFormatStrings.HoursAgo, timeSpan.Hours);
            if (timeSpan.Minutes > 0)
                return string.Format(timeSpan.Minutes == 1 ? dateTimeFormatStrings.MinuteAgo : dateTimeFormatStrings.MinutesAgo, timeSpan.Minutes);
            if (timeSpan.Seconds > 1 || timeSpan.Seconds == 0)
                return string.Format(dateTimeFormatStrings.SecondsAgo, timeSpan.Seconds);
            if (timeSpan.Seconds == 1)
                return string.Format(dateTimeFormatStrings.SecondAgo, timeSpan.Seconds);
            throw new NotSupportedException("The DateTime object does not have a supported value.");
        }
    }
}