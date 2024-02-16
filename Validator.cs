using SharpControls.Utils.Extensions;
using SharpControls.Validation.Validators;
using System.Security.AccessControl;

namespace SharpControls.Validation
{
    public static class Validator
    {
        public static bool Validate(string obj, string validations)
        {
            return Validate(obj, validations.Split('|'));
        }

        public static bool Validate(string obj, params string[] validations)
        {
            foreach(string validation in validations)
            {
                if (!ValidateString(obj, validation))
                    return false;
            }
            return true;
        }

        private static bool ValidateString(string obj, string validation)
        {
            //SIMPLE VALIDATIONS
            switch (validation)
            {
                case "required":
                    return obj.Length != 0 && obj != null;
                case "accepted":
                    return obj == "true" || obj == "1" || obj == "yes";
                case "numeric":
                    return float.TryParse(obj, out _);
                case "integer":
                    return int.TryParse(obj, out _);
                case "alpha":
                    return ValidationRegex.OnlyAlpha().Match(obj).Success;
                case "alpha_dash":
                    return ValidationRegex.OnlyAlphaDash().Match(obj).Success;
                case "email":
                    return Simple.Email(obj);
                case "cpf":
                    return Simple.Cpf(obj);
                case "cnpj":
                    return Simple.Cnpj(obj);
                case "pis":
                    return Simple.Pis(obj);
                case "ip_address":
                    return Simple.IPv4(obj) || Simple.IPv6(obj);
                case "ipv4":
                    return Simple.IPv4(obj);
                case "ipv6":
                    return Simple.IPv6(obj);
                case "uppercase":
                    return obj.All(char.IsUpper);
                case "lowercase":
                    return obj.All(char.IsLower);
                case "url":
                    return Simple.Url(obj);
                case "url_http":
                    return Simple.UrlHttp(obj);
                case "url_https":
                    return Simple.UrlHttps(obj);
                case "url_ftp":
                    return Simple.UrlFtp(obj);
                case "url_ftps":
                    return Simple.UrlFtps(obj);
                case "url_ssh":
                    return Simple.UrlSsh(obj);
                case "url_file":
                    return Simple.UrlFile(obj);
                case "url_mailto":
                    return Simple.UrlMailto(obj);
            }

            //EXTENDED VALIDATIONS
            if (validation.StartsWith("max:"))
            {
                int maxL = int.Parse(validation.ReplaceFirst("max:", ""));
                return obj.Length <= maxL;
            }
            else if (validation.StartsWith("min:"))
            {
                int minL = int.Parse(validation.ReplaceFirst("min:", ""));
                return obj.Length >= minL;
            }
            else if (validation.StartsWith("len:"))
            {
                int length = int.Parse(validation.ReplaceFirst("len:", ""));
                return obj.Length == length;
            }
            else if (validation.StartsWith("between:"))
            {
                var splitted = validation.ReplaceFirst("between:", "").Split(',');
                int from = int.Parse(splitted[0]);
                int to = int.Parse(splitted[1]);
                return obj.Length >= from && obj.Length <= to;
            }
            else if (validation.StartsWith("phone:"))
            {
                string country = validation.ReplaceFirst("phone:", "");
                return Phone.Validate(obj, country);
            }
            else if (validation.StartsWith("cellphone:"))
            {
                string country = validation.ReplaceFirst("phone:", "");
                return Phone.Validate(obj, country, Phone.PhoneType.Cellphone);
            }
            else if (validation.StartsWith("landline:"))
            {
                string country = validation.ReplaceFirst("phone:", "");
                return Phone.Validate(obj, country, Phone.PhoneType.Landline);
            }
            else if (validation.StartsWith("starts_with:"))
            {
                string str = validation.ReplaceFirst("starts_with:", "");
                return obj.StartsWith(str);
            }
            else if (validation.StartsWith("ends_with:"))
            {
                string str = validation.ReplaceFirst("ends_with:", "");
                return obj.EndsWith(str);
            }
            else if (validation.StartsWith("contains:"))
            {
                string str = validation.ReplaceFirst("contains:", "");
                return obj.Contains(str);
            }
            else if (validation.StartsWith("like:"))
            {
                string str = validation.ReplaceFirst("like:", "");
                return obj.Like(str);
            }
            else if (validation.StartsWith("same:"))
            {
                string str = validation.ReplaceFirst("same:", "");
                return obj == str;
            }
            else if (validation.StartsWith("not_same:"))
            {
                string str = validation.ReplaceFirst("same:", "");
                return obj != str;
            }
            throw new Exception("Invalid validation " + validation);
        }
    }
}
