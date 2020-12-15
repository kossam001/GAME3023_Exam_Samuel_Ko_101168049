using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public List<Weather> weathers;
    public Lighting lighting;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartWeatherSystem());
    }

    IEnumerator StartWeatherSystem()
    {
        for (int i = 0; i < weathers.Count; i++)
        {
            // Active time
            weathers[i].gameObject.SetActive(true);
            lighting.SetIntensity(weathers[i].lightIntensity);

            yield return new WaitForSeconds(3);

            yield return StartCoroutine(WeatherTransition(weathers[i], weathers[i + 1]));
            weathers[i].gameObject.SetActive(false);
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

    IEnumerator LerpWeatherParams(Weather from, Weather to)
    {
        yield return null;
    }
}
