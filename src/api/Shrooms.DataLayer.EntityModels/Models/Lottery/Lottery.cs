using System;
using Shrooms.Contracts.Enums;

namespace Shrooms.DataLayer.EntityModels.Models.Lotteries
{
    public class Lottery : BaseModelWithOrg
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public LotteryStatus Status { get; set; }

        public int EntryFee { get; set; }

        public bool IsRefundFailed { get; set; }

        public virtual ImageCollection Images { get; set; }

        public int GiftedTicketLimit { get; set; }
    }
}
