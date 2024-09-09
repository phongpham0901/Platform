using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    [SerializeField] int nextSceneLoad;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.enabled = true;

            /*
            if (SceneManager.GetActiveScene().buildIndex == 12)
            {
                SceneManager.LoadScene(1);
            }
            */
         
                StartCoroutine(DelayLoadLevel());
           
        }

    }

    IEnumerator DelayLoadLevel()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        SceneManager.LoadScene(nextSceneLoad);
        if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", nextSceneLoad);
            //PlayerPrefs.DeleteAll();
        }
    }
}
