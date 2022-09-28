using Shrooms.Contracts.Enums;

namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public class FilterPreset : BaseModelWithOrg
    {
        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public PageType ForPage { get; set; }

        public string Preset { get; set; }
    }
}
