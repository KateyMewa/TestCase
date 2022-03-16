using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace DaysCount
{
    public enum Months
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }
    public class DateTimeValidator
    {
        public static readonly Regex ShortDateFormat = new Regex(@"(?<day>\d+)\/(?<month>\d+)\/(?<year>\d+)");
        public static readonly Regex LongDateFormat = new Regex(@"(?<day>\d+)\s(?<month>\w{3,9})\s(?<year>\d+)");
        Dictionary<Regex, Func<string, string, string, IEnumerable<string>>> validationRules = new Dictionary<Regex, Func<string, string, string, IEnumerable<string>>> {
            {ShortDateFormat, (day, month, year) => {
                int intDay = int.Parse(day);
                int intMonth = int.Parse(month);
                int intYear = int.Parse(year);
                var validationErrors = new List<string>();
                if (!IsYearValid(intYear))
                {
                    validationErrors.Add($"There is no {year}th year!");
                }
                else if (!IsMonthValid(intMonth))
                {
                    validationErrors.Add($"There is no {month}th month!");
                }
                else if (!IsDayValid(intDay, intMonth, intYear))
                {
                    validationErrors.Add("This day does not exists in this month!");
                }
                return validationErrors;
            } },
            {LongDateFormat, (day, month, year) => {
                int intDay = int.Parse(day);

                int intYear = int.Parse(year);
                var validationErrors = new List<string>();
                if (!IsYearValid(intYear))
                {
                    validationErrors.Add($"There is no {year}th year!");
                }
                else if (!IsMonthValid(month))
                {
                    validationErrors.Add("This month does not exists!");
                }
                else
                {
                    int intMonth = (int)(Months)Enum.Parse(typeof(Months), month);
                    if (!IsDayValid(intDay, intMonth, intYear))
                    {
                        validationErrors.Add("This day does not exists in this month!");
                    }
                }
                return validationErrors;
            } }
        };

        public static bool IsDayValid(int day, int month, int year)
        {
            return day <= DateTime.DaysInMonth(year, month) && day > 0;
        }

        public static bool IsMonthValid(int month)
        {
            return month <= 12 && month > 0;
        }

        public static bool IsYearValid(int year)
        {
            return year <= 9999 && year > 0;
        }

        public static bool IsMonthValid(string month)
        {
            return Enum.GetNames(typeof(Months)).ToList().Exists(m => String.Equals(month, m, StringComparison.InvariantCultureIgnoreCase));
        }

        public ValidationResult Validate(string dateTime)
        {
            var validationErrors = new List<string>();
            foreach (var validationRule in validationRules)
            {
                Match match = validationRule.Key.Match(dateTime);
                if (match.Length != 0)
                {
                    string day = match.Groups["day"].Value;
                    string month = match.Groups["month"].Value;
                    string year = match.Groups["year"].Value;
                    validationErrors.AddRange(validationRule.Value(day, month, year));
                }
            }
            return new ValidationResult() { ValidationErrors = validationErrors, IsValid = !validationErrors.Any() };
        }
    }
}
