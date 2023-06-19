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
    #endregion

    #region EVENTS
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
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
        }
    }

    void Rotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }

        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotation)
    {
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
    }
    #endregion
}
