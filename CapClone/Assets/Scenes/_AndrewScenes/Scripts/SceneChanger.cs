using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public bool changeIt;
    public int nextScene;
    // Start is called before the first frame update
    void Start()
    {
        changeIt = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeIt)
        {
            // add a transition effect and a score screen?
            SceneManager.LoadScene(nextScene);
        }
    }
}
