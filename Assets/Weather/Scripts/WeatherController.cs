/*
 * The weather system: controls transitions between weather states;
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [Header("Weather")]
    public Weather currentWeather; // Starting weather
    private Weather nextWeather; // The next weather
    private Weather currentWeatherChecker; // Check if weather changed externally

    [Tooltip("Amount of time to transition from one weather state to another")]
    public float transitionDuration = 1;
    public float weatherTimer;

    [Header("Lighting")]
    public WeatherLighting lighting;

    [Header("Weather System Volume")]
    public float maxVolume = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartWeatherSystem());
    }

    IEnumerator StartWeatherSystem()
    {
        nextWeather = null;

        // Used for overriding current weather
        currentWeatherChecker = currentWeather;
        weatherTimer = currentWeather.weatherDuration;

        // Turn on current weather
        yield return currentWeather.TransitionEffects(true);
        lighting.SetIntensity(currentWeather.lightIntensity);
        currentWeather.SetSoundVolume(maxVolume);

        // Using a timer instead of WaitFor so that the timer can be modified in the inspector
        while (weatherTimer > 0)
        {
            weatherTimer -= Time.deltaTime;
            yield return null;
        }

        float randomRoll = Random.Range(0.0f, 1.0f);
        float chanceSum = 0;

        // Setting next transition to be the last weather in the list be default, in case the user does not use the correct weights
        nextWeather = currentWeather.transitionWeathers[currentWeather.transitionWeathers.Count - 1];

        for (int i = 0; i < Mathf.Min(currentWeather.transitionWeathers.Count, currentWeather.probability.Count); i++)
        {
            chanceSum += currentWeather.probability[i];
            if (randomRoll <= chanceSum)
            {
                nextWeather = currentWeather.transitionWeathers[i];
                break;
            }
        }

        StartCoroutine(TransitionWeather(nextWeather));
    }

    IEnumerator TransitionWeather(Weather nextWeather)
    {
        yield return StartCoroutine(WeatherTransition(currentWeather, nextWeather));

        currentWeather = nextWeather;

        StartCoroutine(StartWeatherSystem());
    }

    // Update is called once per frame
    void Update()
    {
        if (weatherTimer > 0)
        {
            // If the current weather has changed
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

    // Smoothly transition between weather states
    IEnumerator WeatherTransition(Weather from, Weather to)
    {
        StartCoroutine(currentWeather.TransitionLight(from, to, lighting));
        StartCoroutine(currentWeather.TransitionSound(from, to, maxVolume));

        // Wait for weather to completely transition
        yield return StartCoroutine(currentWeather.TransitionEffects(false));
    }
}
