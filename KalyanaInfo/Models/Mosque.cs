using System;
using System.Collections.Generic;

namespace KalyanaInfo.Models
{
    public partial class Mosque
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImamMasjid { get; set; }
        public int Fiqqah { get; set; }
        public string Mobile { get; set; }
        public string Maintainer { get; set; }
        public DateTime BuildDate { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

        public virtual Fiqqah FiqqahNavigation { get; set; }
        public virtual Person User { get; set; }
    }
}
