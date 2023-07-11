using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene((int)Scene.Game);
        GameManager.Instance.currentScene = Scene.Game;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
