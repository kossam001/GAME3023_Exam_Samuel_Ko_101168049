/*
 * Controls global lighting for weather system
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WeatherLighting : MonoBehaviour
{
    private Light2D lighting;

    public float lightIntensity = 1;

    private void Awake()
    {
        lighting = GetComponent<Light2D>();
    }

    public void SetIntensity(float intensity)
    {
        lightIntensity = intensity;
        lighting.intensity = lightIntensity;
    }
}
