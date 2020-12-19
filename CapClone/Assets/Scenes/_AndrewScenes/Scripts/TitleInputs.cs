using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleInputs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("RecentScene",1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) { GameObject.Find("SceneChanger").GetComponent<SceneChanger>().nextScene = PlayerPrefs.GetInt("RecentScene"); GameObject.Find("SceneChanger").GetComponent<SceneChanger>().changeIt = true; }
    }
}
