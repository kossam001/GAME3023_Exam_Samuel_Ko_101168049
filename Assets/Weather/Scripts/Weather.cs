/*
 * Weather consists of lights, effects, and sound.
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    public Effect effect;

    public Vector3 effectRange = new Vector3(1, 1, 1);
    public float effectIntensity = 10;
    public Vector3 effectDirection = new Vector3(1, 1, 1);

    public Lighting lighting;

    public float lightIntensity = 1;
    public float soundVolume;

    private void Update()
    {
        effect.SetRange(effectRange);
        effect.SetIntensity(effectIntensity);
        effect.SetDirection(effectDirection);

        lighting.SetIntensity(lightIntensity);
    }
}
