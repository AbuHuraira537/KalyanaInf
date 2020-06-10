using System;
using System.Collections.Generic;

namespace KalyanaInfo.Models
{
    public partial class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime UploadDate { get; set; }
        public int UserId { get; set; }

        public virtual Person User { get; set; }
    }
}
