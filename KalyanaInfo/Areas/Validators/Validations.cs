using KalyanaInfo.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace KalyanaInfo.Areas.Validators
{
    public class Validations
    {
        public Validations()
        { 
        }
        public string EmailValidation(string email)
        {
           
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return "";
            else
                return "Email is Invalid</br>";
        
        }
        public string MobileValidation(string mobile)
        {
            if(mobile.Length!=11)
            {
                return "Mobile Number is Invalid</br>";
            }
            for (int i=0;i<mobile.Length;i++)
            {
                if(mobile[i]<'0' && mobile[i]>'9')
                {
                    return "Mobile Number is Invalid</br>";
                }
            }
            return "";
        }
        public string DateValidation(DateTime d)
        {
            string date =Convert.ToString(d);
           
            try
            {
                if (DateTime.TryParse(date, out DateTime temp))
                    return "";
                else
                    return "DateTime Invalid</br>";
            }
            catch
            {
                return "DateTime Invalid</br>";

            }
        }
        public string PersonValidation(Person p)
        {
            string error;
            error =EmailValidation(p.Email);
            error += MobileValidation(p.Mobile);
            error += DateValidation(p.DateOfBirth);
            if(p.About.Length<=0 && p.Address.Length<=0 && p.Hobby.Length<=0 && p.Married.Length<=0 && p.Name.Length<=0 && p.Password.Length<=0 &&p.SonOrDaughterOf.Length<=0  )
            {
                error += " and Some fields ae empty</br>";
           
            }
            return error;
        }



    }
}
