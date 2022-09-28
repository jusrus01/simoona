using System;

namespace Shrooms.DataLayer.EntityModels.Models.Multiwalls
{
    public class Comment : LikeBaseModel
    {
        public string MessageBody { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public ImageCollection Images { get; set; }

        public DateTime LastEdit { get; set; }

        public bool IsHidden { get; set; }
    }
}
