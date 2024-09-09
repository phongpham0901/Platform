using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoad : MonoBehaviour
{
    public void loadChooseScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayScene()
    {
        Time.timeScale = 1f;
        //Panel.SetActive(false);
    }

} 
