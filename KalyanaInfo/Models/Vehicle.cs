using System;
using System.Collections.Generic;

namespace KalyanaInfo.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Person = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}
