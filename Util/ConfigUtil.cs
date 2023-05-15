using FluffyUnderware.DevTools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winch.Config;

namespace Chaos.Util;

internal class ConfigUtil
{
    public static Dictionary<string, object?> config = new();
    public static Dictionary<string, object> defaultConfig = new()
    {
        { "secondsBetweenEffects", 15}
    };

    public static Dictionary<string, object?> GetMainConfig()
    {
        defaultConfig.ForEach(item =>
        {
            config[item.Key] = ModConfig.GetProperty("Chaos", item.Key, item.Value);
        });

        return config;
    } 
}
