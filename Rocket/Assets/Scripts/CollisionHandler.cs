using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    #region VARIABLES
    [Header("Scene Management")]
    [SerializeField] float levelLoadDelay = 2f;
    bool isTransitioning = false;

    [Header("Sound Effects")]
    [SerializeField] AudioClip winSFX;
    [SerializeField] AudioClip crashSFX;
    AudioSource environmentAudioSource;
    #endregion

    #region EVENTS
    void Start()
    {
        environmentAudioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other) 
    {
        EnterCollision(other);
    }
    #endregion

    #region METHODS
    void EnterCollision(Collision other)
    {
        if(isTransitioning){ return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is friendly");
                break;
            
            case "Finish":
                StartWinSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        environmentAudioSource.Stop();
        environmentAudioSource.PlayOneShot(crashSFX, 0.3f);
        //TODO: Add particle effect uppon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartWinSequence()
    {
        isTransitioning = true;
        environmentAudioSource.Stop();
        environmentAudioSource.PlayOneShot(winSFX, 0.5f);
        //TODO: Add particle effect uppon win
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        
        SceneManager.LoadScene(nextSceneIndex);
    }
    #endregion
}
