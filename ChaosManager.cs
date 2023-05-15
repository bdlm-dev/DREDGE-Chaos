﻿using CommandTerminal;
using FluffyUnderware.DevTools.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;
using Winch.Core;
using Chaos.Effects;
using Chaos.Util;
using System.Collections;

namespace Chaos;

internal class ChaosManager : MonoBehaviour
{
    public static Dictionary<string, EffectData>? effects;
    public static Dictionary<string, object?>? chaosConfig;
    private void Start()
    {
        WinchCore.Log.Debug("Started Chaos Manager");
        chaosConfig = ConfigUtil.GetMainConfig();
        effects = new AllEffects().effects;
        AddTerminalCommands();
    }

    private void AddTerminalCommands()
    {
        Terminal.Shell.AddCommand("chaos.effect", new Action<CommandArg[]>(CallEffectDebug), 1, 1, "Force chaos effect by id.");
    }

    public void CallEffectDebug(CommandArg[] args)
    {
        bool flag = false;
        WinchCore.Log.Debug($"Calling chaos effect with id ({args[0].String})");
        effects.ForEach(item =>
        {
            if (item.Key == args[0].String)
            {
                StartCoroutine(ExecuteEvent(item.Value, Convert.ToInt32(chaosConfig?["secondsBetweenEffects"])));
                flag = true;
            }
        });
        if (!flag) WinchCore.Log.Debug($"Effect with id ({args[0].String}) not found.");
        return;
    }

    IEnumerator ExecuteEvent(EffectData e, int delay)
    {
        try
        {
            e.Trigger();
            WinchCore.Log.Debug($"Triggered event {e.id}");
        }
        catch (Exception ex)
        {
            WinchCore.Log.Debug($"Error while triggering event {e.id}: {ex}");
        }

        yield return new WaitForSeconds(delay);

        try
        {
            e.Cleanup();
            WinchCore.Log.Debug($"Cleaned up event {e.id}");
        }
        catch (Exception ex)
        {
            WinchCore.Log.Debug($"Error while cleaning up event {e.id}: {ex}");
        }

    }
}