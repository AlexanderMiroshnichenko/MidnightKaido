using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGmusic : MonoBehaviour
{
    public static BGmusic instance;
   
    public AudioSource mainMenuMusic;
    public AudioSource gamePlayMusic;

    void Awake()
    {

        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
   
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
           
            gamePlayMusic.enabled = false;
            mainMenuMusic.enabled = true;
            mainMenuMusic.Play();
           
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            mainMenuMusic.enabled = false;
            gamePlayMusic.enabled = true;
            gamePlayMusic.Play();
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log("Disable Event");
    }

}