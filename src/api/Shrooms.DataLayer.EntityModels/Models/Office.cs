using Shrooms.DataLayer.EntityModels.Models.Books;
using System.Collections.Generic;

namespace Shrooms.DataLayer.EntityModels.Models
{
    public class Office : BaseModelWithOrg
    {
        public bool IsDefault { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        public virtual ICollection<BookOffice> BookOffices { get; set; }

        public virtual ICollection<Floor> Floors { get; set; }
    }
}