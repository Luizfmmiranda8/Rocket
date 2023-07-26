using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    #region VARIABLES
    [Header("Position")]
    Vector3 startingPosition;

    [Header("Movement")]
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    
    #endregion

    #region EVENTS
    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        Vector3 offset = movementVector * movementFactor;

        transform.position = startingPosition + offset;
    }
    #endregion

    #region METHODS
    #endregion
}
