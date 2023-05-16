using Chaos.Util;
using HarmonyLib;
using System;
using UnityEngine;
using Winch.Core;

namespace Chaos.Effects.Player;

public class BackwardsOnly : EffectHandler
{
    public string id = "player_backwards_only";

    [HarmonyPatch(typeof(DredgeInputManager))]
    [HarmonyPatch("GetValue")]
    [HarmonyPatch(new Type[] { typeof(DredgePlayerActionTwoAxis) })]
    class InputGetValuePatcher
    {
        public static void Postfix(ref Vector2 __result)
        {
            if (ChaosManager.activeEffects.Contains("player_backwards_only"))
            {
                __result.y = Math.Min(__result.y, 0);
            }
        }
    }

}
