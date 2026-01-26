using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeTweaker : MonoBehaviour
{
    private Volume _vol;
    private Bloom _bloom;
    private float _bloomIntensity = 0f;

    private void Awake()
    {
        _vol = GetComponent<Volume>();
        _vol.profile.TryGet<Bloom>(out _bloom);
        _bloomIntensity = _bloom.intensity.value;
    }

    private void Update()
    {
        if (EffectDirector.Enables(EffectType.Bloom))
        {
            if (!_bloom.IsActive())
            {
                _bloom.intensity.value = _bloomIntensity;
            }
        }
        else
        {
            if (_bloom.IsActive())
            {
                _bloom.intensity.value = 0f;
            }
        }
    }
}