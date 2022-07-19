using System.Globalization;

namespace ETF_API.Logic
{
    public static class Helpers
    {
        private static readonly Random Randomizer = new();
        public static int GetRandomSleepTimeMilliseconds()
        {
            var RandomDelaySeconds = Randomizer.Next(Global.Settings.iShares.SleepSecondsBetweenRequestsMin * 1000, Global.Settings.iShares.SleepSecondsBetweenRequestsMax * 1000);
            if (RandomDelaySeconds > 0)
            {
                return RandomDelaySeconds;
            }
            else
            {
                return 0;
            }
        }

        public static decimal? ParseDecimal(string Raw)
        {
            var Value = Raw?.Trim();
            if (Value == "-" || string.IsNullOrEmpty(Value))
            {
                return null;
            }
            else
            {
                return decimal.Parse(Value.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
        }

        public static string ParseString(string Raw)
        {
            var Value = Raw?.Trim();
            return Value == "-" ? null : Value;
        }


        public static string GetFullMessage(this Exception source)
        {
            if (source == null)
            {
                return "Empty exception";
            }

            var Errors = new List<string>
            {
                source.Message,
                source.InnerException?.Message,
                source.InnerException?.InnerException?.Message,
                source.InnerException?.InnerException?.InnerException?.Message
            }.ToList();

            return string.Join(" -> ", Errors.Distinct().Where(E => !string.IsNullOrEmpty(E)));
        }

        public static string NullIfEmpty(this string Source) => string.IsNullOrEmpty(Source) ? null : Source;

    }
}
