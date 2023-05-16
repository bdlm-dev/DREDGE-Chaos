using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Winch.Config;

namespace Chaos.Effects;

public class EffectData
{
    public readonly string id;
    public readonly string name;

    public readonly Dictionary<string, object?> data;
    public readonly Action Trigger;
    public readonly Action Cleanup;

    public readonly EffectCategory category;

    public EffectData(string id, string name, Action<bool> function, EffectCategory effectCategory = EffectCategory.GENERIC)
    {
        this.id = id;
        this.name = name;
        this.data = ModConfig.GetFullConfig("Chaos", "Config.json", id);
        category = effectCategory;
        Trigger = () => function(false);
        Cleanup = () => function(true);
    }

    public enum EffectCategory
    {
        GENERIC,
        PLAYER,
        WEATHER,
        ENEMY
    }
}
