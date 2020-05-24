using Cosmos.HAL;

namespace SartoxOS.Utils
{
    public static class UnixTime
    {
        public static string Now()
        {
            return $"{RTC.DayOfTheWeek}/{RTC.Month}/{RTC.Year} {RTC.Hour}:{RTC.Minute}:{RTC.Second}";
        }
    }
}
