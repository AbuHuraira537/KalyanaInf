using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KalyanaInfo.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int MessageFrom { get; set; }
        public int MessageTo { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Person MessageFromNavigation { get; set; }
        public virtual Person MessageToNavigation { get; set; }

    }
}
