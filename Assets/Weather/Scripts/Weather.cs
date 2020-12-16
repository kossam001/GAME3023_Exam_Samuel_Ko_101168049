/*
 * Weather consists of lights, effects, and sound.  The weather state.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    [Header("Weather Setup")]
    public List<Weather> transitionWeathers;

    [Tooltip("Probability of the weather happening between 0 and 1")]
    public List<float> probability;
    public float weatherDuration = 3;

    // For weather transition
    [Header("Weather Effects")]
    [Tooltip("List of particle effects to player during the weather state")]
    public List<Effect> effects;

    [Tooltip("List of effect parameters particular to this weather state")]
    public List<EffectData> effectData;

    private Dictionary<string, EffectData> effectDataDict;

    [Header("Scene Lighting")]
    [Tooltip("The amount of lighting during the weather state")]
    public float lightIntensity = 1;

    [Header("Weather Sounds")]
    public float maxSoundVolume = 1;
    public float currentVolume = 0;
    private AudioSource soundPlayer;

    private void Start()
    {
        effectDataDict = new Dictionary<string, EffectData>();

        soundPlayer = GetComponent<AudioSource>();
        soundPlayer.volume = 0;

        // Create a dictionary for the effect for faster access
        foreach (EffectData data in effectData)
        {
            effectDataDict.Add(data.associatedEffect, data);
        }
    }

    public void ToggleWeather(bool isOn)
    {
        // Active time
        foreach (Effect effect in effects)
        {
            EffectData data = effectDataDict[effect.effectName];

            // Instead of turning the entire particle system off, keep on but stop emission
            // That way particles don't vanish into thin air
            effect.SetIntensity(Convert.ToInt32(isOn) * data.effectIntensity);
        }
    }

    public void SetSoundVolume(float volume)
    {
        currentVolume = volume;
        soundPlayer.volume = Mathf.Min(maxSoundVolume, currentVolume);
    }
}
