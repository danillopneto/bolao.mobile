using System;

namespace Bolao.Pinheiros.BusinessLogic.Utils
{
    public static class TimeZoneUtils
    {
        public static DateTime ToBrasiliaDateTime(this DateTime date)
        {
            return TimeZoneInfo.ConvertTime(date, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
        }
    }
}