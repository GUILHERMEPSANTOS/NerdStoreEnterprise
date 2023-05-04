using System.ComponentModel.DataAnnotations;

namespace Core.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class CreditCardExpired : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is null) return false;

            var month = value.ToString().Split("/")[0];
            var year = $"20{value.ToString().Split('/')[1]}"; ;

            return ValidateDate(year, month);
        }

        private bool ValidateDate(string year, string month)
        {
            if (int.TryParse(year, out int yearInteger) &&
            int.TryParse(month, out int monthInteger))
            {
                var expiredDate = new DateTime(year: yearInteger, month: monthInteger, 1);

                return expiredDate > DateTime.UtcNow;
            }

            return false;
        }
    }
}