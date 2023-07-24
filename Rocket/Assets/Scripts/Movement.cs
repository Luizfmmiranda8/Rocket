using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region VARIABLES
    [Header("Physics")]
    Rigidbody rocketRigidbody;

    [Header("Movement")]
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;

    [Header("Sound Effects")]
    [SerializeField] AudioClip mainEngineSFX;
    AudioSource rocketAudioSource;

    [Header("Partcile System")]
    [SerializeField] ParticleSystem mainThrustEffect;
    [SerializeField] ParticleSystem rightThrustEffect;
    [SerializeField] ParticleSystem leftThrustEffect;
    #endregion

    #region EVENTS
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        rocketAudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        Thrust();
        Rotation();
    }
    #endregion

    #region METHODS
    void Thrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rocketRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!rocketAudioSource.isPlaying)
            {
                rocketAudioSource.PlayOneShot(mainEngineSFX);
                mainThrustEffect.Play();
            }
        }
        else
        {
            if(rocketAudioSource.isPlaying)
            {
                rocketAudioSource.Stop();
                mainThrustEffect.Stop();
            }
        }
    }

    void Rotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            
            if(!rightThrustEffect.isPlaying)
            {
                rightThrustEffect.Play();
            }
        }

        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);

            if(!leftThrustEffect.isPlaying)
            {
                leftThrustEffect.Play();        
            }
        }

        else
        {
            rightThrustEffect.Stop();
            leftThrustEffect.Stop();
        }
    }

    void ApplyRotation(float rotation)
    {
        rocketRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        rocketRigidbody.freezeRotation = false;
    }
    #endregion
}
