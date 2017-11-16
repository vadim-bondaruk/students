using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StudentsApp
{
    public class NameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var strValue = value as string;

            if (string.IsNullOrWhiteSpace(strValue))
                return new ValidationResult(false, "Поле обязательно для заполнения");

            string pattern = @"[a-zA-zа-яА-Я]+([ '-][a-zA-Zа-яА-Я]+)*";

            Regex regex = new Regex(pattern);

                if (!regex.IsMatch(strValue))
                {
                    return new ValidationResult(false,
                        "Недопустимые символы для имени");
                }


            return new ValidationResult(true, null);
        }
    }
}
