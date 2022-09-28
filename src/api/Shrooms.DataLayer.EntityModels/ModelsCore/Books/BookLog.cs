using System;

namespace Shrooms.DataLayer.EntityModels.ModelsCore.Books
{
    public class BookLog : BaseModelWithOrg
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public DateTime TakenFrom { get; set; }

        public DateTime? Returned { get; set; }

        public int BookOfficeId { get; set; }

        public BookOffice BookOffice { get; set; }
    }
}
