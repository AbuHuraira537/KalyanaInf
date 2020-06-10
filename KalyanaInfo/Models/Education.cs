using System;
using System.Collections.Generic;

namespace KalyanaInfo.Models
{
    public partial class Education
    {
        public Education()
        {
            Person = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Upto { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}
