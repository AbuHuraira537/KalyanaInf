using System;
using System.Collections.Generic;

namespace KalyanaInfo.Models
{
    public partial class SchoolType
    {
        public SchoolType()
        {
            School = new HashSet<School>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<School> School { get; set; }
    }
}
