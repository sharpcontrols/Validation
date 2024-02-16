using SharpControls.Utils.Extensions;
using SharpControls.Validation.Validators;
using System.Security.AccessControl;

namespace SharpControls.Validation
{
    public static class Validator
    {
        #region NUMERIC VALIDATIONS
        public static bool Validate(float obj, string validations)
        {
            return Validate(obj, validations.Split('|'));
        }

        public static bool Validate(float obj, params string[] validations)
        {
            foreach (string validation in validations)
            {
                if (!ValidateNumbers(obj, validation))
                    return false;
            }
            return true;
        }

        public static bool ValidateNumbers(float? obj, string validation)
        {
            //SIMPLE VALIDATIONS
            switch (validation)
            {
                case "required":
                    return obj != null;
                case "positive":
                    return obj > 0;
                case "negative":
                    return obj < 0;
            }

            //EXTENDED VALIDATIONS
            if (validation.StartsWith("max:"))
            {
                float max = float.Parse(validation.ReplaceFirst("max:"));
                return obj <= max;
            }
            else if (validation.StartsWith("min:"))
            {
                float min = float.Parse(validation.ReplaceFirst("min:"));
                return obj >= min;
            }
            else if (validation.StartsWith("between:"))
            {
                var splitted = validation.ReplaceFirst("between:").Split(',');
                float from = float.Parse(splitted[0]);
                float to = float.Parse(splitted[1]);
                return obj >= from && obj <= to;
            }
            else if (validation.StartsWith("equal:"))
            {
                int nbr = int.Parse(validation.ReplaceFirst("equal:"));
                return obj == nbr;
            }
            else if (validation.StartsWith("not_equal:"))
            {
                int nbr = int.Parse(validation.ReplaceFirst("not_equal:"));
                return obj != nbr;
            }

            throw new Exception("Invalid validation " + validation);
        }
        #endregion

        #region DATE VALIDATIONS
        public static bool Validate(DateTime obj, string validations)
        {
            return Validate(obj, validations.Split('|'));
        }

        public static bool Validate(DateTime obj, params string[] validations)
        {
            foreach (string validation in validations)
            {
                if (!ValidateDateTime(obj, validation))
                    return false;
            }
            return true;
        }

        public static bool ValidateDateTime(DateTime obj, string validation)
        {
            //SIMPLE VALIDATIONS
            switch (validation)
            {
                case "future":
                    return obj > DateTime.Now;
                case "future_date":     
                    return obj.Date > DateTime.Now.Date;
                case "past":
                    return obj < DateTime.Now;
                case "past_date":
                    return obj.Date < DateTime.Now.Date;
                case "now":
                    return obj == DateTime.Now;
                case "today":
                    return obj.Date == DateTime.Now.Date;
                case "this_month":
                    return obj.Month == DateTime.Now.Month;
                case "this_year":
                    return obj.Year == DateTime.Now.Year;

            }

            //EXTENDED VALIDATIONS
            if (validation.StartsWith("before:"))
            {
                DateTime max = DateTime.Parse(validation.ReplaceFirst("max:"));
                return obj <= max;
            }
            else if (validation.StartsWith("after:"))
            {
                DateTime min = DateTime.Parse(validation.ReplaceFirst("min:"));
                return obj >= min;
            }
            else if (validation.StartsWith("between:"))
            {
                var splitted = validation.ReplaceFirst("between:").Split(',');
                DateTime from = DateTime.Parse(splitted[0]);
                DateTime to = DateTime.Parse(splitted[1]);
                return obj >= from && obj <= to;
            }
            else if (validation.StartsWith("month:"))
            {
                int month = int.Parse(validation.ReplaceFirst("month:"));
                return obj.Month == month;
            }
            else if (validation.StartsWith("year:"))
            {
                int year = int.Parse(validation.ReplaceFirst("year:"));
                return obj.Year == year;
            }
            else if (validation.StartsWith("equal:"))
            {
                DateTime dtE = DateTime.Parse(validation.ReplaceFirst("equal:"));
                return obj == dtE;
            }
            else if (validation.StartsWith("not_equal:"))
            {
                DateTime dtE = DateTime.Parse(validation.ReplaceFirst("not_equal:"));
                return obj != dtE;
            }
            else if (validation.StartsWith("equal_date:"))
            {
                DateTime dtE = DateTime.Parse(validation.ReplaceFirst("equal_date:"));
                return obj.Date == dtE.Date;
            }
            else if (validation.StartsWith("not_equal_date:"))
            {
                DateTime dtE = DateTime.Parse(validation.ReplaceFirst("not_equal_date:"));
                return obj.Date != dtE.Date;
            }

            throw new Exception("Invalid validation " + validation);
        }
        #endregion

        #region STRING VALIDATIONS
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
                case "datetime":
                    return DateTime.TryParse(obj, out _);
            }

            //EXTENDED VALIDATIONS
            if (validation.StartsWith("max:"))
            {
                int maxL = int.Parse(validation.ReplaceFirst("max:"));
                return obj.Length <= maxL;
            }
            else if (validation.StartsWith("min:"))
            {
                int minL = int.Parse(validation.ReplaceFirst("min:"));
                return obj.Length >= minL;
            }
            else if (validation.StartsWith("len:"))
            {
                int length = int.Parse(validation.ReplaceFirst("len:"));
                return obj.Length == length;
            }
            else if (validation.StartsWith("between:"))
            {
                var splitted = validation.ReplaceFirst("between:").Split(',');
                int from = int.Parse(splitted[0]);
                int to = int.Parse(splitted[1]);
                return obj.Length >= from && obj.Length <= to;
            }
            else if (validation.StartsWith("phone:"))
            {
                string country = validation.ReplaceFirst("phone:");
                return Phone.Validate(obj, country);
            }
            else if (validation.StartsWith("cellphone:"))
            {
                string country = validation.ReplaceFirst("cellphone:");
                return Phone.Validate(obj, country, Phone.PhoneType.Cellphone);
            }
            else if (validation.StartsWith("landline:"))
            {
                string country = validation.ReplaceFirst("landline:");
                return Phone.Validate(obj, country, Phone.PhoneType.Landline);
            }
            else if (validation.StartsWith("starts_with:"))
            {
                string str = validation.ReplaceFirst("starts_with:");
                return obj.StartsWith(str);
            }
            else if (validation.StartsWith("ends_with:"))
            {
                string str = validation.ReplaceFirst("ends_with:");
                return obj.EndsWith(str);
            }
            else if (validation.StartsWith("contains:"))
            {
                string str = validation.ReplaceFirst("contains:");
                return obj.Contains(str);
            }
            else if (validation.StartsWith("like:"))
            {
                string str = validation.ReplaceFirst("like:");
                return obj.Like(str);
            }
            else if (validation.StartsWith("equal:"))
            {
                string str = validation.ReplaceFirst("equal:");
                return obj == str;
            }
            else if (validation.StartsWith("not_equal:"))
            {
                string str = validation.ReplaceFirst("not_equal:");
                return obj != str;
            }
            throw new Exception("Invalid validation " + validation);
        }
        #endregion
    }
}
