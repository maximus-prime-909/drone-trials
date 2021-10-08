using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f; //for rotation
    [SerializeField] float mainThrust = 100f; //for thrust
    [SerializeField] float levelLoadDelay = 2f;

   [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;

    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject winPrefab;

    Rigidbody rigidBody;
    AudioSource audioSource;

    public enum State {  Alive, Dying, Transcending }
    State state = State.Alive;

    bool collisionsDisabled= false; //for debug

    Vector3 screenBounds; //for out of frame death [ADRIFT]
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        //todo somewhere stop sound on death
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }

        if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();   //todo only if debug on i.e commented
        }


        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (gameObject.transform.position.y >= screenBounds.y + 0.5 || gameObject.transform.position.y <= (screenBounds.y * -1) - 0.5)
        {
            StartDeathSequence();
            
        }
    }


    private void RespondToDebugKeys() // only if debug 
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionsDisabled = !collisionsDisabled;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || collisionsDisabled) // // only for debug
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
               
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                //kill player
                StartDeathSequence();
                break;
        }
    }

   
    private void StartSuccessSequence()
    {
        if (winPrefab)
        {
            // Instantiate an explosion effect at the gameObjects position and rotation
            Instantiate(winPrefab, transform.position, Quaternion.identity);
        }
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        //deathParticles.Play();
        if (explosionPrefab)
        {
            // Instantiate an explosion effect at the gameObjects position and rotation
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }
        enabled = false;
        Invoke("LoadThisLevel", levelLoadDelay);
    }

    private void LoadNextLevel() //Level loop
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; //loop back to start
        }
         SceneManager.LoadScene(nextSceneIndex); 
        
    }

    private void LoadThisLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) //can thrust while rotating
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
           // mainEngineParticles.Stop();
        }
    }
    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
           // mainEngineParticles.Play();
        }
    }
    
    
    private void RespondToRotateInput()
    {

        rigidBody.freezeRotation = true; 
       
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
        {
           
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; 
    }
    
}
