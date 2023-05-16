using Chaos.Util;
using System;
using Winch.Core;

namespace Chaos.Effects.Player;

public class SpeedBoost : EffectHandler
{
    public string id = "player_speed_boost";

    public float defaultSpeedMultiplier;
    public float speedMultiplier = 5f;

    public override void Trigger()
    {
        this.defaultSpeedMultiplier = Convert.ToSingle(PlayerUtil.defaultPlayerControllerData["_baseMovementModifier"]);
        speedMultiplier = Convert.ToSingle(ConfigUtil.GetEffectProperty(this.id, "speedMultiplier", (double) this.speedMultiplier));

        WinchCore.Log.Debug($"Called speedboost with properties {this.defaultSpeedMultiplier}, {this.speedMultiplier}");

        if (PlayerUtil.playerController == null)
        {
            WinchCore.Log.Error("Found null playerController");
            return;
        }

        PlayerUtil.playerController._baseMovementModifier = 
            Convert.ToSingle(PlayerUtil.defaultPlayerControllerData["_baseMovementModifier"]) * this.speedMultiplier;
    }

    public override void Cleanup()
    {
        if (PlayerUtil.playerController == null)
        {
            WinchCore.Log.Error("Found null playerController");
            return;
        }
        PlayerUtil.playerController._baseMovementModifier = Convert.ToSingle(PlayerUtil.defaultPlayerControllerData["_baseMovementModifier"]);
        WinchCore.Log.Debug("Cleaned up speedboost");
    }
}
