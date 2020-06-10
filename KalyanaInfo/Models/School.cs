using System;
using System.Collections.Generic;

namespace KalyanaInfo.Models
{
    public partial class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int UserId { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Images { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Principal { get; set; }
        public string PrincipalPicture { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual SchoolType TypeNavigation { get; set; }
        public virtual Person User { get; set; }
    }
}
