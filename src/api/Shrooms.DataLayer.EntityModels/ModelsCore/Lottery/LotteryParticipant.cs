using System;

namespace Shrooms.DataLayer.EntityModels.ModelsCore.Lotteries
{
    public class LotteryParticipant : BaseModel
    {
        public int LotteryId { get; set; }

        public string UserId { get; set; }

        public DateTime Joined { get; set; }

        public virtual ApplicationUser User { get; set; }

        public Lottery Lottery { get; set; }
    }
}
