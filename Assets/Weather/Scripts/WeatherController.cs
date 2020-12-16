using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherCondition
{
    SUNNY,
    OVERCAST,
    RAINING,
    THUNDERSTORM
}

public class WeatherController : MonoBehaviour
{
    public Weather currentWeather; // Starting weather
    public Lighting lighting;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartWeatherSystem());
    }

    IEnumerator StartWeatherSystem()
    {
        while (true)
        {


            // Turn on current weather
            currentWeather.ToggleWeather(true);
            lighting.SetIntensity(currentWeather.lightIntensity);

            yield return new WaitForSeconds(currentWeather.weatherDuration);

            // Transition to next weather state

            Weather nextWeather = currentWeather.transitionWeathers[Random.Range(0, currentWeather.transitionWeathers.Count)];
            yield return StartCoroutine(WeatherTransition(currentWeather, nextWeather));

            // Turn off current weather
            currentWeather.ToggleWeather(false);
            currentWeather = nextWeather;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/#how_to_use_lerp_in_unity
    IEnumerator WeatherTransition(Weather from, Weather to)
    {
        float duration = 1.0f;
        float elapsedTime = 0.0f;
        float lightIntensity = from.lightIntensity;

        while (elapsedTime <= duration)
        {
            lightIntensity = Mathf.Lerp(from.lightIntensity, to.lightIntensity, elapsedTime / duration);
            lighting.SetIntensity(lightIntensity);
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        //StartCoroutine()


        //from.effectDirection = to.effectDirection;
        //from.effectIntensity = to.effectIntensity;
        //from.effectRange = to.effectRange;

        //from.lightIntensity = to.lightIntensity;
    }
}
