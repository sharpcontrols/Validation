using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharpControls.Validation.Validators
{
    internal static class Phone
    {
        internal enum PhoneType
        {
            All,
            Landline,
            Cellphone
        }

        internal static bool Validate(string phoneNumber, string countryCode, PhoneType type = PhoneType.All)
        {
            if (countryCode == "bra")
            {
                if (phoneNumber.StartsWith("+55"))
                {
                    phoneNumber = phoneNumber[2..];
                }
                string number = ValidationRegex.OnlyNumbers().Replace(phoneNumber, "");
                if (type == PhoneType.All || type == PhoneType.Landline)
                {
                    if(number.Length == 10)
                    {
                        return true;
                    }
                }
                if(type == PhoneType.All || type == PhoneType.Cellphone)
                {
                    if(number.Length == 11)
                    {
                        return true;
                    }
                }
            }
            else
            {
                throw new NotImplementedException("Country " + countryCode + " not implemented.");
            }
            return false;
        }
    }
}
