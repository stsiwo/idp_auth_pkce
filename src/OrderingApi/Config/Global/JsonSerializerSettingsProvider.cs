using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Config.Global
{
    public static class JsonSerializerSettingsProvider
    {
        public static JsonSerializerSettings IgnoreLoopSetting = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
    }
}
