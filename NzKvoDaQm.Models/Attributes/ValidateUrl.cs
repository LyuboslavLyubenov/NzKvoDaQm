namespace NzKvoDaQm.Models.Attributes
{

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class ValidateUrlAttribute : ValidationAttribute
    {
        private const string Pattern = "(https?:\\/\\/(?:www\\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\\.[^\\s]{2,}|www\\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\\.[^\\s]{2,}|https?:\\/\\/(?:www\\.|(?!www))[a-zA-Z0-9]\\.[^\\s]{2,}|www\\.[a-zA-Z0-9]\\.[^\\s]{2,})";

        public override bool IsValid(object value)
        {
            var url = (string)value;
            return !string.IsNullOrWhiteSpace(url) && Regex.IsMatch(url, Pattern);
        }
    }
}
