using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EffectData", menuName ="WeatherData")]
public class EffectData : ScriptableObject
{
    public string associatedEffect;

    public Vector3 effectRange = new Vector3(1, 1, 1);
    public float effectIntensity = 10;
    public Vector3 effectDirection = new Vector3(1, 1, 1);
}
