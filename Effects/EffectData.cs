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
    public readonly bool isPatch;

    public readonly Dictionary<string, object?> data;
    public EffectHandler effectInstance = new();
    public readonly Action Trigger = () => { };
    public readonly Action Cleanup = () => { };

    public readonly EffectCategory category;

    public EffectData(string id, string name, EffectHandler effectObject, EffectCategory effectCategory = EffectCategory.GENERIC)
    {
        this.id = id;
        this.name = name;
        data = ModConfig.GetFullConfig("Chaos", "Config.json", id);
        effectInstance = effectObject;
        category = effectCategory;
        Trigger = () => effectObject.Handle(false);
        Cleanup = () => effectObject.Handle(true);
    }

    public enum EffectCategory
    {
        GENERIC,
        PLAYER,
        WEATHER,
        ENEMY
    }
}
