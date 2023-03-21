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
    private void Start()
    {
     
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
  

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenuMusic.enabled = true;
            gamePlayMusic.enabled = false;
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            mainMenuMusic.enabled = false;
            gamePlayMusic.enabled = true;
        }
    }

}