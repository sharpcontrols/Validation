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
                    return obj.Length != 0;
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
            }

            //EXTENDED VALIDATIONS
            if (validation.StartsWith("max:"))
            {
                int maxL = int.Parse(validation.Replace("max:", ""));
                return obj.Length <= maxL;
            }
            else if (validation.StartsWith("min:"))
            {
                int minL = int.Parse(validation.Replace("min:", ""));
                return obj.Length >= minL;
            }
            else if (validation.StartsWith("len:"))
            {
                int length = int.Parse(validation.Replace("len:", ""));
                return obj.Length == length;
            }
            else if (validation.StartsWith("between:"))
            {
                var splitted = validation.Replace("between:", "").Split(',');
                int from = int.Parse(splitted[0]);
                int to = int.Parse(splitted[1]);
                return obj.Length >= from && obj.Length <= to;
            }
            else if (validation.StartsWith("phone:"))
            {
                string country = validation.Replace("phone:", "");
                return Phone.Validate(obj, country);
            }
            else if (validation.StartsWith("cellphone:"))
            {
                string country = validation.Replace("phone:", "");
                return Phone.Validate(obj, country, Phone.PhoneType.Cellphone);
            }
            else if (validation.StartsWith("landline:"))
            {
                string country = validation.Replace("phone:", "");
                return Phone.Validate(obj, country, Phone.PhoneType.Landline);
            }
            else if (validation.StartsWith("starts_with:"))
            {
                string str = validation.Replace("starts_with:", "");
                return obj.StartsWith(str);
            }
            else if (validation.StartsWith("ends_with:"))
            {
                string str = validation.Replace("ends_with:", "");
                return obj.EndsWith(str);
            }
            else if (validation.StartsWith("contains:"))
            {
                string str = validation.Replace("contains:", "");
                return obj.Contains(str);
            }
            else if (validation.StartsWith("like:"))
            {

            }
            throw new Exception("Invalid validation " + validation);
        }
    }
}
