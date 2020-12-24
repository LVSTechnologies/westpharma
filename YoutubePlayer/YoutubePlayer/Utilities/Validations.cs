using System;
using System.Text.RegularExpressions;

namespace YoutubePlayer.Utilities
{
    public static class Validations
    {
        const string emailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        public static bool ValidEmail(this string email)
        {

            if (Regex.Match(email, emailRegex).Success)
                return true;

            return false;
        }
    }
}
