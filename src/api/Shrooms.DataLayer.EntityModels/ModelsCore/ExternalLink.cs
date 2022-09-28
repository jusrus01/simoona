using Shrooms.Contracts.Enums;

namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public class ExternalLink : BaseModelWithOrg
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public ExternalLinkTypeEnum Type { get; set; }

        public int Priority { get; set; }
    }
}
