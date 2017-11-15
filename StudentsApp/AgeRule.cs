using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StudentsApp
{
    public class AgeRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int age = 0;

            try
            {
                age = Int32.Parse((string)value);
            }
            catch
            {
                return new ValidationResult(false, "Недопустимые символы.");
            }

            if ((age < 16) || (age > 100))
            {
                return new ValidationResult(false,
                  "Возраст не входит в диапазон " + 16 + " до " + 100 + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
