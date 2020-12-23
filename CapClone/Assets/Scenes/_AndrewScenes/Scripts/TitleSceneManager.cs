using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleSceneManager : MonoBehaviour
{
    public int sceneToLoad;

    public static bool switchScene;

    public string[] sceneNames;
    public string activeName;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        activeName = sceneNames[sceneToLoad];
        if (SceneManager.GetActiveScene().name == "TransitionalScene")
        {
            GameObject.Find("TITLE").GetComponent<TextMeshProUGUI>().text = activeName;
            if (PlayerPrefs.GetFloat(sceneToLoad.ToString() + "time")>0)
            {
                float time = PlayerPrefs.GetFloat(sceneToLoad.ToString() + "time");
                time *= 100;
                time = Mathf.Round(time);
                time /= 100;
                GameObject.Find("BESTTIME").GetComponent<TextMeshProUGUI>().text = "Best Time: " + time.ToString() + "s";
            }
            if (switchScene)
            {
                if (GameObject.Find("FADE"))
                {
                    if (GameObject.Find("FADE").GetComponent<Image>().color.a >= 1)
                    {
                        SceneManager.LoadScene(sceneToLoad);
                    }
                }
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    switchScene = true;
                }
            }
        }
    }
}
