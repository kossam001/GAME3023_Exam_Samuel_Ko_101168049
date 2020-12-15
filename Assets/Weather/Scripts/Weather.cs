/*
 * Weather consists of lights, effects, and sound.
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allow for multiple weather effects by making it a list of weathers
[System.Serializable]
public class WeatherState
{
    public List<Weather> weather;
}

public class Weather : MonoBehaviour
{
    // For weather transition
    public List<WeatherState> weatherStates;
    public Effect effect;

    public Vector3 effectRange = new Vector3(1, 1, 1);
    public float effectIntensity = 10;
    public Vector3 effectDirection = new Vector3(1, 1, 1);

    //public Lighting lighting;

    public float lightIntensity = 1;
    public float soundVolume;

    public float weatherDuration = 3;

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
