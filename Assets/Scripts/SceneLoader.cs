using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int scene_id)
    {
        SceneManager.LoadScene(scene_id);
    }
}
