﻿using CommandTerminal;
using HarmonyLib;
using System;
using UnityEngine;
using Winch;
using Winch.Config;
using Winch.Core;
using Winch.Core.API;
using Winch.Core.API.Events;
using Winch.Core.API.Events.Addressables;

namespace Chaos;

public static class Loader
{
    public static GameObject? chaosManagerObject;
    public static ChaosManager? chaosManagerScript;
    public static void Start()
    {
        if (!ModConfig.GetProperty("Chaos", "enabled", true))
        {
            WinchCore.Log.Debug("Chaos Mod Disabled");
            return;
        }

        DredgeEvent.AddressableEvents.WorldEventsLoaded.On += InstantiateChaosManager;
    }

    public static void InstantiateChaosManager(object sender, AddressablesLoadedEventArgs<WorldEventData> _)
    {
        GameObject eventManagerObject = new()
        {
            name = "Chaos Manager"
        };
        chaosManagerScript = eventManagerObject.AddComponent<ChaosManager>();

        if (chaosManagerObject == null)
        {
            chaosManagerObject = eventManagerObject;
        } 
        else
        {
            GameObject.Destroy(chaosManagerObject);
            chaosManagerObject = eventManagerObject;
        }
    }
}