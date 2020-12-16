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
    [Tooltip("Do not change this during runtime")]
    public Weather currentWeather; // Starting weather

    [Tooltip("Set this to change current weather")]
    public Weather nextWeather; // The next weather
    private Weather currentWeatherChecker; // Check if weather changed externally

    public Lighting lighting;
    public float weatherTimer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartWeatherSystem());
    }

    IEnumerator StartWeatherSystem()
    {
        nextWeather = null;

        currentWeatherChecker = currentWeather;
        weatherTimer = currentWeather.weatherDuration;

        // Turn on current weather
        currentWeather.ToggleWeather(true);
        lighting.SetIntensity(currentWeather.lightIntensity);

        // Using a timer instead of WaitFor so that the timer can be modified in the inspector
        while (weatherTimer > 0)
        {
            weatherTimer -= Time.deltaTime;

            yield return null;
        }

        nextWeather = currentWeather.transitionWeathers[Random.Range(0, currentWeather.transitionWeathers.Count)];
        StartCoroutine(TransitionWeather(nextWeather));
    }

    IEnumerator TransitionWeather(Weather nextWeather)
    {
        yield return StartCoroutine(WeatherTransition(currentWeather, nextWeather));

        // Turn off current weather
        currentWeather.ToggleWeather(false);
        currentWeather = nextWeather;

        StartCoroutine(StartWeatherSystem());
    }

    // Update is called once per frame
    void Update()
    {
        if (nextWeather != null && weatherTimer > 0)
        {
            StopAllCoroutines();
            weatherTimer = -1000;
            StartCoroutine(TransitionWeather(nextWeather));
        }
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
    }
}
