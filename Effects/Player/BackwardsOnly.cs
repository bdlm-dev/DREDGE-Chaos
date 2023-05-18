using Chaos.Util;
using HarmonyLib;
using System;
using UnityEngine;

namespace Chaos.Effects.Player;

public class BackwardsOnly : EffectHandler
{
    public static string id = "backwards_only";

    [HarmonyPatch(typeof(DredgeInputManager))]
    [HarmonyPatch("GetValue")]
    [HarmonyPatch(new Type[] { typeof(DredgePlayerActionTwoAxis) })]
    class BackwardsInputGetValuePatcher
    {
        public static void Postfix(ref Vector2 __result)
        {
            if (ChaosManager.activeEffects.Contains(id))
            {
                __result.y = Math.Min(__result.y, 0);
            }
        }
    }

    public override void Trigger()
    {
        PlayerUtil.playerController._baseReverseModifier = 1.5f;
    }
    public override void Cleanup()
    {
        PlayerUtil.playerController._baseReverseModifier = (float) PlayerUtil.defaultPlayerControllerData["_baseReverseModifier"];
    }
}
