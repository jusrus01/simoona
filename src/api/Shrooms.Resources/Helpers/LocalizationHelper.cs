using Newtonsoft.Json;

namespace Shrooms.Resources.Helpers
{
    public static class LocalizationHelper
    {
        public static string ToJson(this object item)
        {
            return JsonConvert.SerializeObject(item);
        }
    }
}