using System;
using System.Collections.Generic;

namespace KalyanaInfo.Models
{
    public partial class Shope
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Images { get; set; }
        public int ShopeType { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }

        public virtual ShopeTypes ShopeTypeNavigation { get; set; }
        public virtual Person User { get; set; }
    }
}
