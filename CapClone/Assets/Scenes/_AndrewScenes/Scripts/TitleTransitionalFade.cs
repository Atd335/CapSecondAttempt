using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTransitionalFade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TitleSceneManager.switchScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (TitleSceneManager.switchScene)
        {
            GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, new Color(0,0,0,1.1f), Time.deltaTime * 10);
        }
        else
        {
            GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, new Color(0, 0, 0, -.1f), Time.deltaTime * 10);
        }
    }
}
