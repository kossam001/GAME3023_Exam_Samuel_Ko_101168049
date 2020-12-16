using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public Weather currentWeather; // Starting weather
    private Weather nextWeather; // The next weather
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
        currentWeather.SetSoundVolume(1);

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
        if (weatherTimer > 0)
        {
            if (!ReferenceEquals(currentWeather, currentWeatherChecker))
            {
                // Reworking the references so that system does not get confused
                nextWeather = currentWeather;
                currentWeather = currentWeatherChecker;

                StopAllCoroutines();
                weatherTimer = -1000;
                StartCoroutine(TransitionWeather(nextWeather));
            }
        }
    }

    // https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/#how_to_use_lerp_in_unity
    IEnumerator WeatherTransition(Weather from, Weather to)
    {
        float duration = 1.0f;
        float elapsedTime = 0.0f;
        float lightIntensity = from.lightIntensity;

        float fromVolume = from.soundVolume;
        float toVolume = to.soundVolume;

        while (elapsedTime <= duration)
        {
            lightIntensity = Mathf.Lerp(from.lightIntensity, to.lightIntensity, elapsedTime / duration);
            lighting.SetIntensity(lightIntensity);

            // Turn off current sound
            from.SetSoundVolume(Mathf.Lerp(fromVolume, 0, elapsedTime / duration));

            // Turn on next sound
            to.SetSoundVolume(Mathf.Lerp(toVolume, 1, elapsedTime / duration));

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
