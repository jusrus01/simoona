using Shrooms.Contracts.Enums;

namespace TemporaryDataLayer
{
    public class KudosType : BaseModel
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public KudosTypeEnum Type { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
