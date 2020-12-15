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
        yield return new WaitForSeconds(1);
        lighting.SetIntensity(originalIntensity);
    }
}
