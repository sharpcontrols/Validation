using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpControls.Validation.Validators
{
    internal static class Simple
    {
        //Copied and modified from: https://stackoverflow.com/a/1374644
        internal static bool Email(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith('.'))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        //Copied and modified from: https://www.macoratti.net/11/09/c_val1.htm
        internal static bool Cpf(string cpf)
        {
            int[] multiplicator1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
            int[] multiplicator2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];
            string tempCpf;
            string digit;
            int sum;
            int rest;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf[..9];
            sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplicator1[i];

            rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = rest.ToString();

            tempCpf += digit;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplicator2[i];

            rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit += rest.ToString();

            return cpf.EndsWith(digit);
        }

        //Copied and modified from: https://www.macoratti.net/11/09/c_val1.htm
        internal static bool Cnpj(string cnpj)
        {
            int[] multiplicator1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
            int[] multiplicator2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
            int sum;
            int rest;
            string digit;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj[..12];

            sum = 0;
            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplicator1[i];

            rest = (sum % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = rest.ToString();

            tempCnpj += digit;
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplicator2[i];

            rest = (sum % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit += rest.ToString();

            return cnpj.EndsWith(digit);
        }

        //Copied and modified from: https://www.macoratti.net/11/09/c_val1.htm
        internal static bool Pis(string pis)
        {
            int[] multiplicator = [3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
            int sum;
            int rest;

            if (pis.Trim().Length != 11)
                return false;

            pis = pis.Trim();
            pis = pis.Replace("-", "").Replace(".", "").PadLeft(11, '0');


            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(pis[i].ToString()) * multiplicator[i];

            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            return pis.EndsWith(rest.ToString());
        }
    }
}
