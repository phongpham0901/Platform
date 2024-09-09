using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] float Score;
    [SerializeField] float highScore;
    [SerializeField] Text textScore;
    [SerializeField] Text textHighScore;
    [SerializeField] GameObject Panel;
    public static GameManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        highScore = PlayerPrefs.GetFloat("highScore" + currentSceneIndex);
        Debug.Log("Current scene index: " + currentSceneIndex);
        //PlayerPrefs.DeleteAll();
    }

    public void increment()
    {
        Score+= 150;
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = Score.ToString();
        textHighScore.text = highScore.ToString();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (Score > highScore)
        {
            PlayerPrefs.SetFloat("highScore" + currentSceneIndex, Score);
            //PlayerPrefs.DeleteAll();
        }
        //PlayerPrefs.DeleteAll();
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Play()
    {
        Time.timeScale = 1f;
        Panel.SetActive(false);
    }

    public void ActivePanelTrue()
    {
        Panel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
