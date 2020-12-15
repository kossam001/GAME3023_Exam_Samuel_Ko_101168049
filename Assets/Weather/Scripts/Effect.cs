/*
 * Manipulates the particle system of the weather.
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    protected ParticleSystem weatherEffect;
    protected ParticleSystem.ShapeModule particleShape;
    protected ParticleSystem.EmissionModule particleEmission;
    protected ParticleSystem.VelocityOverLifetimeModule particleVelocity;
    protected List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    [Header("Effect Transform")]
    public Vector3 effectRange = new Vector3(1, 1, 1);

    [Header("Effect Emission")]
    public float effectIntensity;  

    [Header("Effect Direction")]
    public Vector3 effectDirection = new Vector3(1, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        weatherEffect = GetComponent<ParticleSystem>();

        particleShape = weatherEffect.shape;
        particleEmission = weatherEffect.emission;
        particleVelocity = weatherEffect.velocityOverLifetime;
    }

    public void SetRange(Vector3 range)
    {
        particleShape.scale = range;
    }

    public void SetIntensity(float intensity)
    {
        effectIntensity = intensity;
        particleEmission.rateOverTime = effectIntensity;
    }

    public void SetDirection(Vector3 direction)
    {
        particleVelocity.x = direction.x;
        particleVelocity.y = direction.y;
        particleVelocity.z = direction.z;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //particleShape.enabled = true;
    //    particleShape.shapeType = ParticleSystemShapeType.Box;
    //    particleShape.scale = effectRange;

    //    particleEmission.rateOverTime = effectIntensity;

    //    particleVelocity.x = effectDirection.x;
    //    particleVelocity.y = effectDirection.y;
    //    particleVelocity.z = effectDirection.z;
    //}

    protected virtual void OnParticleCollision(GameObject other)
    {

    }
}