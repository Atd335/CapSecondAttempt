using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public bool changeIt;
    public int nextScene;
    SpriteRenderer[] boxes;

    float scaleTimer;

    public GameObject pauseManager;
    bool pSpawned;

    public GameObject titleManager;

    private void Awake()
    {
        if (pauseManager) { Instantiate(pauseManager, transform); }
        if (titleManager && !(GameObject.Find("TitleSceneManager(Clone)"))) { Instantiate(titleManager); }
    }

    // Start is called before the first frame update
    void Start()
    {
        changeIt = false;
        boxes = GetComponentsInChildren<SpriteRenderer>();
        scaleTimer = 1;
        foreach (SpriteRenderer b in boxes)
        {
            if (b.name[0] == 'S') b.color = Color.black;
        }
}

    // Update is called once per frame
    void Update()
    {
        if (changeIt)
        {
            scaleTimer = Mathf.Lerp(scaleTimer, 1.1f, Time.deltaTime * 10);
            scaleTimer = Mathf.Clamp(scaleTimer, 0, 1);
            foreach (SpriteRenderer b in boxes)
            {
                b.transform.localScale = Vector3.Lerp(Vector3.one * 0, Vector3.one * 2,scaleTimer);

            }
            // add a transition effect and a score screen?
            if (scaleTimer == 1)
            {
                GameObject.Find("TitleSceneManager(Clone)").GetComponent<TitleSceneManager>().sceneToLoad = nextScene;
                // set the high score in playerprefs?
                SceneManager.LoadScene("TransitionalScene");
            }
        }
        else
        {

            scaleTimer = Mathf.Lerp(scaleTimer, -.1f, Time.deltaTime * 3);
            scaleTimer = Mathf.Clamp(scaleTimer, 0, 1);
            foreach (SpriteRenderer b in boxes)
            {
                if (b.name[0]=='S')
                {
                    b.transform.localScale = Vector3.Lerp(Vector3.one * 2, Vector3.one * 0, 1 - scaleTimer);
                }
            }
        }


    }
}
