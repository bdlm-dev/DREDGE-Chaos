using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaos.Effects;

public class AllEffects
{
    public Dictionary<string, EffectData> effects = new() 
    {
        { "player_speed_boost", new EffectData("player_speed_boost", "Speed Boost", new Player.SpeedBoost().Handle) },
    };
}
