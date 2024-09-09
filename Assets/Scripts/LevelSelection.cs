using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button[] lvlbutton;

    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 2);
        //PlayerPrefs.DeleteAll();
        for (int i = 0; i < lvlbutton.Length; i++)
        {
            if (i + 2 > levelAt)
            {
                lvlbutton[i].interactable = false;
                //PlayerPrefs.DeleteAll();
            }
            //PlayerPrefs.DeleteAll();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
