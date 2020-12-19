using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManagementScript : MonoBehaviour
{
    public static bool paused;

    public SpriteRenderer darkness;

    float menuTimer;

    public AnimationCurve menuUp;
    public Transform menu;

    public static bool muted;
    public static float senstivity = 1;
    public Collider2D mute;
    public Collider2D sens;
    public Collider2D main;

    public SpriteRenderer[] sensBlocks;

    // Start is called before the first frame update
    void Awake()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { paused = !paused; }

        if (paused)
        {
            mute.offset = new Vector2(Random.Range(-.0001f,0),0);
            sens.offset = new Vector2(Random.Range(-.0001f, 0), 0);
            main.offset = new Vector2(Random.Range(-.0001f, 0), 0);
            menuTimer += Time.unscaledDeltaTime * 4;
            menuTimer = Mathf.Clamp(menuTimer,0,1);
            buttonSelect();
            Time.timeScale = 0;
            darkness.color = Color.Lerp(darkness.color, new Color(0,0,0,0.5f), Time.unscaledDeltaTime * 10);
        }
        else
        {
            mute.offset = new Vector2(Random.Range(-.0001f, 0), 0);
            sens.offset = new Vector2(Random.Range(-.0001f, 0), 0);
            menuTimer -= Time.unscaledDeltaTime * 4;
            menuTimer = Mathf.Clamp(menuTimer, 0, 1);

            Time.timeScale = 1;
            darkness.color = Color.Lerp(darkness.color, new Color(0, 0, 0,  0), Time.unscaledDeltaTime * 10);
        }
        for (int i = 0; i < sensBlocks.Length; i++)
        {
            sensBlocks[i].enabled = (i <= PlayerPrefs.GetFloat("ScrollSpeed") / 5);
        }

        menu.transform.localPosition = new Vector3(0,-(1-menuUp.Evaluate(menuTimer))*10,1);
    }

    RaycastHit2D hit;
    public Camera pauseCam;
    void buttonSelect()
    {
        hit = Physics2D.Raycast(pauseCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider!=null)
        {
            if (hit.collider.tag=="Mutebutton" && Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (GameObject.Find("Music Player"))
                {
                    if (GameObject.Find("Music Player").GetComponent<AudioSource>().volume == 0.1f)
                    {
                        GameObject.Find("Music Player").GetComponent<AudioSource>().volume = 0;
                    }
                    else
                    {
                        GameObject.Find("Music Player").GetComponent<AudioSource>().volume = 0.1f;
                    }
                }
            }
            if (hit.collider.tag == "SensButton" && Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("HEY");
                PlayerPrefs.SetFloat("ScrollSpeed",PlayerPrefs.GetFloat("ScrollSpeed")+5);
                if (PlayerPrefs.GetFloat("ScrollSpeed")>20)
                {
                    PlayerPrefs.SetFloat("ScrollSpeed",0);
                }
            }
            if (hit.collider.tag == "sendToMain" && Input.GetKeyDown(KeyCode.Mouse0))
            {
                SceneManager.LoadScene(11);
            }
        }
    }
}
