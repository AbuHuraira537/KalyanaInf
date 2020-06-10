using System;
using System.Collections.Generic;

namespace KalyanaInfo.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Person = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string GenderName { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}
