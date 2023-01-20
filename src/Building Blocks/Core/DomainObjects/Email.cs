using System.Text.RegularExpressions;

namespace Core.DomainObjects
{
    public class Email
    {
        public const int EmailMaxLength = 254;
        public const int EmailMinLength = 5;
        public string Address { get; private set; }
        protected Email()
        {
        }
        public Email(string address)
        {
            if (Validate(address)) throw new DomainException("E-mail inválido");
            Address = address;
        }

        public static bool Validate(string email)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }
    }
} 