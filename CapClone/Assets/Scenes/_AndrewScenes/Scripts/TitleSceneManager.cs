using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    public int sceneToLoad;

    public static bool switchScene;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "TransitionalScene")
        {
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
