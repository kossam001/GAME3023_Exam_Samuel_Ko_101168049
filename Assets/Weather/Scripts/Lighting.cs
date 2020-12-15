using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Lighting : MonoBehaviour
{
    private Light2D lighting;

    public float lightIntensity = 1;

    private void Start()
    {
        lighting = GetComponent<Light2D>();
    }

    public void SetIntensity(float intensity)
    {
        lighting.intensity = intensity;
    }
}
