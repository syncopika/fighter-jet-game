using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    // https://blog.insane.engineer/post/unity_button_load_scene/
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
