using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : Effect
{
    public Lighting lighting;

    protected override void OnParticleCollision(GameObject other)
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        float originalIntensity = lighting.lightIntensity;
        lighting.SetIntensity(2);

        yield return new WaitForSeconds(0.5f);

        // Change the ligtning spawn rate so it is more random
        // Need a check because the particle system does not immediately 
        // stop when intensity is set to 0
        if (effectIntensity > 0)
        {
            SetIntensity(Random.Range(0.1f, 0.7f));
        }
        lighting.SetIntensity(originalIntensity);
    }
}
