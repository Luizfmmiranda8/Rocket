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
    [SerializeField] float period = 2f;
    float movementFactor;
    
    #endregion

    #region EVENTS
    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        MoveObstacle();
    }
    #endregion

    #region METHODS
    void MoveObstacle()
    {
        if(period <= Mathf.Epsilon){ return; }
        
        float cycles = Time.time / period;
        const float TAU = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * TAU);

        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;

        transform.position = startingPosition + offset;
    }
    #endregion
}
