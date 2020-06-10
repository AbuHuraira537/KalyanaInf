using System;
using System.Collections.Generic;

namespace KalyanaInfo.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string PostType { get; set; }
        public int UserId { get; set; }

        public virtual Person User { get; set; }
    }
}
