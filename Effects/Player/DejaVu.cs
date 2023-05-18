using AeLa.EasyFeedback.APIs;
using Chaos.Util;
using Cinemachine;
using CommandTerminal;
using HarmonyLib;
using Sirenix.Utilities;
using System;
using Winch.Core;
using static Cinemachine.CinemachineFreeLook;

#pragma warning disable CS8602 
// Suppress 'Dereference of a possibly null reference'
// Is keen on saying that playerUtil objects can be null,
// That is managed in ChaosManager.

namespace Chaos.Effects.Player;

public class DejaVu : EffectHandler
{
    public static string id = "deja_vu";

    public static float defaultFOV = 40f;
    public static float customFOV = 80f;
    public static float customHeight = 1f;
    public static float customRadius = 3f;

    public CinemachineCore.AxisInputDelegate? storeDelegate;
    public static CinemachineFreeLook.Orbit[]? defaultOrbits;
    public LensSettings? lens;

    public override void Trigger()
    {
        // Consider increasing turn rate as well
        try
        {
            // Fetch defaultFOV
            defaultFOV = (float) PlayerUtil.playerCamera.defaultFOV;

            // Fetch defaultOrbits
            defaultOrbits = (CinemachineFreeLook.Orbit[])PlayerUtil.playerCamera?.cinemachineCamera.GetType().GetField("m_Orbits").GetValue(PlayerUtil.playerCamera.cinemachineCamera);
            
            // Create custom orbits - must have three
            CinemachineFreeLook.Orbit[] orbits = 
            {
                new CinemachineFreeLook.Orbit(customHeight, customRadius),
                new CinemachineFreeLook.Orbit(customHeight, customRadius),
                new CinemachineFreeLook.Orbit(customHeight, customRadius)
            };

            // Set orbits to custom orbits
            PlayerUtil.playerCamera?.cinemachineCamera.GetType().GetField("m_Orbits").SetValue(PlayerUtil.playerCamera?.cinemachineCamera, orbits);

            // Set custom FOV;
            PlayerUtil.playerCamera.cinemachineCamera.m_Lens.FieldOfView = customFOV;

            // Store player camera input delegate
            storeDelegate = (CinemachineCore.AxisInputDelegate)CinemachineCore.GetInputAxis.Clone();

            // Disable player camera input
            CinemachineCore.GetInputAxis = (input) => { return 0f; };

            // Apply speed boost
            Loader.chaosManagerScript.CallEffect("player_speed_boost");
        }
        catch (Exception ex) 
        {
            WinchCore.Log.Debug(ex);
        }
    }

    public override void Cleanup()
    {
        // Restore player camera input delegate
        CinemachineCore.GetInputAxis = storeDelegate;

        // Restore default player camera orbits
        PlayerUtil.playerCamera?.cinemachineCamera.GetType().GetField("m_Orbits").SetValue(
            PlayerUtil.playerCamera?.cinemachineCamera,
            defaultOrbits
            );

        // Restore default FOV
        PlayerUtil.playerCamera.cinemachineCamera.m_Lens.FieldOfView = defaultFOV;
    }
}