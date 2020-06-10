using System;
using System.Collections.Generic;

namespace KalyanaInfo.Models
{
    public partial class UserLog
    {
        public int UserId { get; set; }
        public string UserIp { get; set; }
        public DateTime? UserLoginTime { get; set; }
        public string Location { get; set; }

        public virtual Person User { get; set; }
    }
}
