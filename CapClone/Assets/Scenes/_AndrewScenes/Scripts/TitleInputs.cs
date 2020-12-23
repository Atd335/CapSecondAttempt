using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleInputs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("RecentScene",1);
        if (PlayerPrefs.GetInt("SetSens")==0) { PlayerPrefs.SetFloat("ScrollSpeed", 10); PlayerPrefs.SetInt("SetSens",1); }
        //Debug.Log(PlayerPrefs.SetFloat("ScrollSpeed"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            PlayerPrefs.SetInt("RecentScene",1);
            PlayerPrefs.SetInt("SetSens", 0);
            for (int i = 0; i < 15; i++)
            {
                PlayerPrefs.SetInt(i.ToString(),0);
                PlayerPrefs.SetFloat(i.ToString()+"time",0);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        if (Input.GetKeyDown(KeyCode.Return)) { GameObject.Find("SceneChanger").GetComponent<SceneChanger>().nextScene = PlayerPrefs.GetInt("RecentScene"); GameObject.Find("SceneChanger").GetComponent<SceneChanger>().changeIt = true; }
        if (Input.GetKeyDown(KeyCode.Tab)) { GameObject.Find("SceneChanger").GetComponent<SceneChanger>().nextScene = 15; GameObject.Find("SceneChanger").GetComponent<SceneChanger>().changeIt = true; }
        //Debug.Log(PlayerPrefs.SetFloat("ScrollSpeed"));
    }
}
