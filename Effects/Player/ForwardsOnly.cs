using Chaos.Util;
using HarmonyLib;
using System;
using UnityEngine;
using Winch.Core;

namespace Chaos.Effects.Player;

public class ForwardsOnly : EffectHandler
{
    public static string id = "player_forwards_only";

    [HarmonyPatch(typeof(DredgeInputManager))]
    [HarmonyPatch("GetValue")]
    [HarmonyPatch(new Type[] { typeof(DredgePlayerActionTwoAxis) })]
    class ForwardsInputGetValuePatcher
    {
        public static void Postfix(ref Vector2 __result)
        {
            if (ChaosManager.activeEffects.Contains(id))
            {
                __result.y = Math.Max(__result.y, 0);
            }
        }
    }

}
