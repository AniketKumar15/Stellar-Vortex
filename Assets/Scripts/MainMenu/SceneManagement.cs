using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    public void playBtn(int sceneNo = 1)
    {
        SceneManager.LoadScene(sceneNo);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
