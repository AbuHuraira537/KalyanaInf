using System;
using System.Collections.Generic;

namespace KalyanaInfo.Models
{
    public partial class Fiqqah
    {
        public Fiqqah()
        {
            Mosque = new HashSet<Mosque>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Mosque> Mosque { get; set; }
    }
}
