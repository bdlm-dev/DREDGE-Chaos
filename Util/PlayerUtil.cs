﻿using FluffyUnderware.DevTools.Extensions;
using HarmonyLib;
using System;
using System.Collections.Generic;
using Winch.Core;
using System.Threading.Tasks;
using CommandTerminal;
using System.Reflection;
using UnityEngine;

namespace Chaos.Util;

public class PlayerUtil
{
    public static Dictionary<string, object?> defaultPlayerStatsData = new();
    public static Dictionary<string, object?> defaultPlayerData = new();
    public static Dictionary<string, object?> defaultPlayerControllerData = new();
    public static Dictionary<string, object?> defaultPlayerCameraData = new();

    public static PlayerStats? playerStats { get; set; }
    public static Player? player { get; set; }
    public static PlayerController? playerController { get; set; }
    public static PlayerCamera? playerCamera { get; set; }


    public static void Load()
    {
        WinchCore.Log.Debug("Loading default player stats");
        LoadPlayerStats();
        LoadPlayer();
    }

    // move LoadPlayerStats to run inside LoadPlayer so retry unnecessary.
    public static async void LoadPlayerStats()
    {
        var fetchPlayerStats = GameManager.Instance.PlayerStats;
        if (fetchPlayerStats != null)
        {
            playerStats = fetchPlayerStats;
            fetchPlayerStats.GetType().GetProperties().ForEach(field => defaultPlayerStatsData[field.Name] = field.GetValue(fetchPlayerStats));
            WinchCore.Log.Debug($"Successfully loaded {defaultPlayerStatsData.Count} playerstats stats.");
            Terminal.Log($"[CHAOS] Successfully loaded PLAYERSTATS data.");
        }
        else
        {
            WinchCore.Log.Debug("Failed loading playerStats, retrying...");
            await Task.Delay(3000);
            LoadPlayerStats();
        }
    }

    public static async void LoadPlayer()
    {
        var fetchPlayer = GameManager.Instance.Player;
        if (fetchPlayer != null)
        {
            player = fetchPlayer;
            fetchPlayer.GetType().GetProperties().ForEach(field => defaultPlayerData[field.Name] = field.GetValue(fetchPlayer));
            WinchCore.Log.Debug($"Successfully loaded {defaultPlayerData.Count} player stats.");
            Terminal.Log($"[CHAOS] Successfully loaded PLAYER data.");
            LoadPlayerController(fetchPlayer);
            LoadPlayerCamera();
        }
        else
        {
            WinchCore.Log.Debug("Failed loading player, retrying...");
            Terminal.Log("Failed loading player, retrying...");
            await Task.Delay(3000);
            LoadPlayer();
        }
    }

    public static void LoadPlayerController(Player player)
    {
        try
        {
            var fetchPlayerController = player.Controller;
            playerController = fetchPlayerController;
            playerController?.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).ForEach(field => defaultPlayerControllerData[field.Name] = field.GetValue(playerController));
            WinchCore.Log.Debug($"Successfully loaded {defaultPlayerControllerData.Count} playercontroller stats.");
            Terminal.Log($"[CHAOS] Successfully loaded PLAYERCONTROLLER data.");
        }
        catch (Exception ex)
        {
            WinchCore.Log.Debug(ex);
        }
    }

    public static void LoadPlayerCamera()
    {
        playerCamera = GameManager.Instance.PlayerCamera;
        playerCamera?.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).ForEach(field => defaultPlayerCameraData[field.Name] = field.GetValue(playerCamera));
    }

    public static void RefreshPlayerData()
    {
        var refreshMethod = AccessTools.Method(typeof(PlayerStats), "CalculateAllStats");
        refreshMethod.Invoke(GameManager.Instance.PlayerStats, null);
        WinchCore.Log.Debug("Re-calculated player stats.");
    }
}
