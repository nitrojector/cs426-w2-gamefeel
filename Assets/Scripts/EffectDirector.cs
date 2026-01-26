using System;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    ScreenShake,
    Bloom,
    SFX,
    Particle,
    Expand
}

public class EffectDirector : MonoBehaviour
{
    public static Array allEffectTypes = Enum.GetValues(typeof(EffectType));

    private readonly Dictionary<EffectType, KeyCode> _effectToggleKeys = new()
    {
        { EffectType.ScreenShake, KeyCode.Alpha1 },
        { EffectType.Bloom, KeyCode.Alpha2 },
        { EffectType.SFX, KeyCode.Alpha3 },
        { EffectType.Particle, KeyCode.Alpha4 },
        { EffectType.Expand, KeyCode.Alpha5 }
    };

    private readonly HashSet<EffectType> _enabledEffects = new();
    public static EffectDirector Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Update()
    {
        foreach (var (type, key) in _effectToggleKeys)
            if (Input.GetKeyDown(key))
                Toggle(type);

        if (Input.GetKeyDown(KeyCode.Equals))
            EnableAll();

        if (Input.GetKeyDown(KeyCode.Minus))
            DisableAll();
    }

    private void EnableAll()
    {
        foreach (EffectType type in allEffectTypes)
            _enabledEffects.Add(type);
    }

    private void DisableAll()
    {
        _enabledEffects.Clear();
    }

    private void Toggle(EffectType type)
    {
        if (_enabledEffects.Contains(type))
            _enabledEffects.Remove(type);
        else
            _enabledEffects.Add(type);
    }

    public static bool Enables(EffectType type)
    {
        return Instance._enabledEffects.Contains(type);
    }
}