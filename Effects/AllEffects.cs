using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chaos.Effects.EffectData;

namespace Chaos.Effects;

public class AllEffects
{
    public Dictionary<string, EffectData> effects = new() 
    {
        { "player_speed_boost", new EffectData("player_speed_boost", "Zoomies", new Player.SpeedBoost(), EffectCategory.PLAYER) },
        { "player_backwards_only", new EffectData("player_backwards_only", "Retreat", new Player.BackwardsOnly(), EffectCategory.PLAYER) },
        { "player_forwards_only", new EffectData("player_forwards_only", "Never Surrender", new Player.ForwardsOnly(), EffectCategory.PLAYER) }
    };
}
