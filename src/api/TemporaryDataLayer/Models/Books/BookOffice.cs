using System.Collections.Generic;

namespace TemporaryDataLayer
{
    public class BookOffice : BaseModelWithOrg
    {
        public virtual ICollection<BookLog> BookLogs { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public int OfficeId { get; set; }

        public Office Office { get; set; }

        public int Quantity { get; set; }
    }
}
