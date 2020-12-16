/*
 * Weather consists of lights, effects, and sound.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    public List<Weather> transitionWeathers;
    public float weatherDuration = 3;

    // For weather transition
    public List<Effect> effects;
    public List<EffectData> effectData;

    private Dictionary<string, EffectData> effectDataDict;

    //public Vector3 effectRange = new Vector3(1, 1, 1);
    //public float effectIntensity = 10;
    //public Vector3 effectDirection = new Vector3(1, 1, 1);

    //public Lighting lighting;

    public float lightIntensity = 1;
    public float soundVolume;

    private void Start()
    {
        effectDataDict = new Dictionary<string, EffectData>();

        // Create a dictionary for the effect for faster access
        foreach (EffectData data in effectData)
        {
            effectDataDict.Add(data.associatedEffect, data);
        }
    }

    public void ToggleWeather(bool isOn)
    {
        //gameObject.SetActive(true);
        // Active time
        foreach (Effect effect in effects)
        {
            EffectData data = effectDataDict[effect.effectName];

            // Instead of turning the entire particle system off, keep on but stop emission
            // That way particles don't vanish into thin air
            effect.SetIntensity(Convert.ToInt32(isOn) * data.effectIntensity);
        }
    }

    //private void Update()
    //{
    //    if (effect != null)
    //    {
    //        effect.SetRange(effectRange);
    //        effect.SetIntensity(effectIntensity);
    //        effect.SetDirection(effectDirection);
    //    }

    //    if (lighting != null)
    //    {
    //        lighting.SetIntensity(lightIntensity);
    //    }
    //}
}
