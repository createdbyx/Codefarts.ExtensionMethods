namespace System
{
    /// <summary>
    /// Provides various helper methods for the <see cref="Random"/> class.
    /// </summary>
    public static class DateTimeExtensionMethods
    {
        public static TimeSpan Hours(this double hours)
        {
            return TimeSpan.FromHours(hours);
        }

        public static TimeSpan Minutes(this double mins)
        {
            return TimeSpan.FromMinutes(mins);
        }

        public static DateTime January(this int day, int year)
        {
            return new DateTime(year, 1, day);
        }

        public static DateTime Febuary(this int day, int year)
        {
            return new DateTime(year, 2, day);
        }

        public static DateTime March(this int day, int year)
        {
            return new DateTime(year, 3, day);
        }

        public static DateTime April(this int day, int year)
        {
            return new DateTime(year, 4, day);
        }

        public static DateTime May(this int day, int year)
        {
            return new DateTime(year, 5, day);
        }

        public static DateTime June(this int day, int year)
        {
            return new DateTime(year, 6, day);
        }

        public static DateTime July(this int day, int year)
        {
            return new DateTime(year, 7, day);
        }

        public static DateTime August(this int day, int year)
        {
            return new DateTime(year, 8, day);
        }

        public static DateTime September(this int day, int year)
        {
            return new DateTime(year, 9, day);
        }

        public static DateTime October(this int day, int year)
        {
            return new DateTime(year, 10, day);
        }

        public static DateTime November(this int day, int year)
        {
            return new DateTime(year, 11, day);
        }

        public static DateTime December(this int day, int year)
        {
            return new DateTime(year, 12, day);
        }
    }
}