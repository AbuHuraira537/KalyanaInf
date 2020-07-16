using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KalyanaInfo.Models
{
    public class Administrator
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public string Password { get; set; }
               public Boolean Login(string key,string password)
        {
            if(key==this.Key && password==this.Password)
            {
                return true;
            }
            return false;
        }



    }
}
