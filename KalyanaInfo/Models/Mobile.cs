using System;
using System.Collections.Generic;

namespace KalyanaInfo.Models
{
    public partial class Mobile
    {
        public Mobile()
        {
            Person = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string MobileType { get; set; }
        public string HasMobile { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}
