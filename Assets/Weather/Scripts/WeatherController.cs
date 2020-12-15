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

    IEnumerator StartWeatherSystem()
    {
        foreach (Weather weather in weathers)
        {
            weather.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            weather.gameObject.SetActive(false);
        }
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

        from.lightIntensity = to.lightIntensity;
    }

    IEnumerator LerpWeatherParams(Weather from, Weather to)
    {
        yield return null;
    }
}
