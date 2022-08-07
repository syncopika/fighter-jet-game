using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    // https://blog.insane.engineer/post/unity_button_load_scene/
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneFromGameManager()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager)
        {
            SceneManager.LoadScene(gameManager.transform.GetComponent<GameManager>().getSelectedMap());
        }
    }
}
