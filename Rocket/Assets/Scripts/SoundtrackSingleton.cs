using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackSingleton : MonoBehaviour
{
    #region VARIABLES
    [Header("Singleton")]
    static SoundtrackSingleton instanceSoundtrack;
    #endregion

    #region EVENTS
    void Awake()
    {
        ManageSingleton();
    }
    #endregion

    #region METHODS
    void ManageSingleton()
    {
        if(instanceSoundtrack != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instanceSoundtrack = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion
}
