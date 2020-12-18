/*
 * Manipulates the particle system of the weather.
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public string effectName;

    // Parameters from the ParticleSystem
    protected ParticleSystem weatherEffect;
    protected ParticleSystem.ShapeModule particleShape;
    protected ParticleSystem.EmissionModule particleEmission;
    protected ParticleSystem.VelocityOverLifetimeModule particleVelocity;
    // For collision events
    protected List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    [Header("Effect Transform")]
    public Vector3 effectRange = new Vector3(1, 1, 1);

    [Header("Effect Emission")]
    public float effectIntensity; // Changes
    //public float maxIntensity; // Mostly Fixed

    [Header("Effect Direction")]
    public Vector3 effectDirection = new Vector3(1, 1, 1);

    // Start is called before the first frame update
    void Awake()
    {
        weatherEffect = GetComponent<ParticleSystem>();

        // Initialize weather effect
        if (weatherEffect != null)
        {
            particleShape = weatherEffect.shape;
            particleEmission = weatherEffect.emission;
            particleVelocity = weatherEffect.velocityOverLifetime;

            SetRange(effectRange);
            SetIntensity(effectIntensity);
            SetDirection(effectDirection);
        }
    }

    public void SetRange(Vector3 range)
    {
        effectRange = range;
        particleShape.scale = effectRange;
    }

    public void SetIntensity(float intensity)
    {
        effectIntensity = intensity;
        particleEmission.rateOverTime = effectIntensity;
    }

    public void SetDirection(Vector3 direction)
    {
        effectDirection = direction;

        particleVelocity.x = effectDirection.x;
        particleVelocity.y = effectDirection.y;
        particleVelocity.z = effectDirection.z;
    }

    protected virtual void OnParticleCollision(GameObject other) {}
}