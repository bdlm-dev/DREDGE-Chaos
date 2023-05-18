using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chaos.Effects.EffectData;

namespace Chaos.Effects;

public class AllEffects
{
    private static readonly List<EffectData> effectInfo = new() 
    {
        { new EffectData("speed_boost", "Zoomies", new Player.SpeedBoost(), EffectCategory.PLAYER) },
        { new EffectData("backwards_only", "Retreat", new Player.BackwardsOnly(), EffectCategory.PLAYER) },
        { new EffectData("forwards_only", "Never Surrender", new Player.ForwardsOnly(), EffectCategory.PLAYER) },
        { new EffectData("deja_vu", "Deja Vu", new Player.DejaVu(), EffectCategory.PLAYER) }
    };

    public static Dictionary<string, EffectData> Generate()
    {
        Dictionary<string, EffectData> effects = new();

        effectInfo.ForEach(e =>
        {
            var generatedId = $"{e.category.ToString().ToLower()}_{e.id}";
            effects[generatedId] = e;
        });

        return effects;
    }
}
