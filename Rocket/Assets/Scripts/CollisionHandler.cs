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

    [Header("Partcile System")]
    [SerializeField] ParticleSystem successEffect;
    [SerializeField] ParticleSystem crashEffect;

    [Header("Collision Manager")]
    bool collisionDisabled = false;
    #endregion

    #region EVENTS
    void Start()
    {
        environmentAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }


    void OnCollisionEnter(Collision other) 
    {
        EnterCollision(other);
    }

    #endregion

    #region METHODS
    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            //Toggle collision
            collisionDisabled = !collisionDisabled;
        }
    }

    void EnterCollision(Collision other)
    {
        if(isTransitioning || collisionDisabled){ return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
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
        crashEffect.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartWinSequence()
    {
        isTransitioning = true;
        environmentAudioSource.Stop();
        environmentAudioSource.PlayOneShot(winSFX, 0.5f);
        successEffect.Play();
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
