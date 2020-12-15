using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public List<Weather> weathers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WeatherTransition(Weather from, Weather to)
    {
        //StartCoroutine()
        from.effectDirection = to.effectDirection;
        from.effectIntensity = to.effectIntensity;
        from.effectRange = to.effectRange;
    }

    IEnumerator LerpWeatherParams(Weather from, Weather to)
    {
        yield return null;
    }
}
