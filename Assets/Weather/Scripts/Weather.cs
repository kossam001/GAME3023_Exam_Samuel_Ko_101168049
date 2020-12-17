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
    public float transitionDuration = 1;
    public bool useInstantEffectTransition; // Particle Emission goes from 0 to max instantly instead of LERP

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

    public IEnumerator TransitionEffects(bool isOn)
    {
        // Active time
        foreach (Effect effect in effects)
        {
            float startingTransitionAmount = effect.effectIntensity;

            EffectData data = effectDataDict[effect.effectName];

            // Instead of turning the entire particle system off, keep on but stop emission
            // That way particles don't vanish into thin air
            if (useInstantEffectTransition)
            {
                effect.SetIntensity(Convert.ToInt32(isOn) * data.effectIntensity);
            }
            else
            {
                StartCoroutine(TransitionEffect(startingTransitionAmount, Convert.ToInt32(isOn) * data.effectIntensity, effect));
            }
        }

        // Wait for the effect to transition
        yield return new WaitForSeconds(transitionDuration);
    }


    // https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/#how_to_use_lerp_in_unity
    IEnumerator TransitionEffect(float start, float end, Effect effect)
    {
        float duration = transitionDuration;
        float elapsedTime = 0;
        float transitionAmount = start;

        while (elapsedTime <= duration)
        {
            transitionAmount = Mathf.Lerp(start, end, elapsedTime / duration);
            effect.SetIntensity(transitionAmount);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    public IEnumerator TransitionLight(Weather from, Weather to, WeatherLighting weatherLighting)
    {
        float duration = transitionDuration;
        float elapsedTime = 0.0f;
        float lightIntensity = from.lightIntensity;

        while (elapsedTime <= duration)
        {
            // Transition scene lighting
            lightIntensity = Mathf.Lerp(from.lightIntensity, to.lightIntensity, elapsedTime / duration);
            weatherLighting.SetIntensity(lightIntensity);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator TransitionSound(Weather from, Weather to, float globalMaxVolume)
    {
        float duration = transitionDuration;
        float elapsedTime = 0.0f;

        float fromVolume = from.currentVolume;
        float toVolume = to.currentVolume;

        while (elapsedTime <= duration)
        {
            // Turn off current sound
            from.SetSoundVolume(Mathf.Lerp(fromVolume, 0, elapsedTime / duration));

            // Turn on next sound
            to.SetSoundVolume(Mathf.Lerp(toVolume, Mathf.Min(globalMaxVolume, to.maxSoundVolume), elapsedTime / duration));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Round out the volume
        from.SetSoundVolume(0);
        to.SetSoundVolume(Mathf.Min(globalMaxVolume, to.maxSoundVolume));
    }

    public void SetSoundVolume(float volume)
    {
        currentVolume = volume;
        soundPlayer.volume = Mathf.Min(maxSoundVolume, currentVolume);
    }
}
