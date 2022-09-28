using System;
using System.Collections.Generic;

namespace Shrooms.DataLayer.EntityModels.Models.Multiwalls
{
    public class Post : LikeBaseModel
    {
        public string MessageBody { get; set; }

        public DateTime LastActivity { get; set; }

        public DateTime LastEdit { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public ImageCollection Images { get; set; }

        public bool IsHidden { get; set; }

        public string SharedEventId { get; set; }
        
        public int WallId { get; set; }

        public Wall Wall { get; set; }
    }
}
