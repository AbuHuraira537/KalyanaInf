using System;
using System.Collections.Generic;

namespace KalyanaInfo.Models
{
    public partial class ShopeTypes
    {
        public ShopeTypes()
        {
            Shope = new HashSet<Shope>();
        }

        public int Id { get; set; }
        public string ShopeName { get; set; }

        public virtual ICollection<Shope> Shope { get; set; }
    }
}
