using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StickPivots : MonoBehaviour
{

    public bool qIsPivot;

    public Transform qPivot;
    public Transform ePivot;
    public Transform trail;

    public Transform eText;
    public Transform qText;

    public float speed;
    public float setSpeed;
    public float dashSpeed;
    public float dashSlow;

    public float scrollSpeed;

    public int rotDir = 1;

    LineRenderer LR;

    public bool inStage;


    RaycastHit2D hit;
    RaycastHit2D activeHit;
    RaycastHit2D staticHit;

    GameObject goal;

    void Start()
    {
        if (GameObject.Find("GOAL"))
        {
            goal = GameObject.Find("GOAL");
        }
        LR = GetComponent<LineRenderer>();
        //stickLength = 1.5f;
    }

    public float stickLength;
    RaycastHit stickHit;

    public float flipTime;
    float flipTimer;
    bool flipped;

    Transform activePivot;
    Transform staticPivot;

    void Update()
    {
        if (scrollSpeed!=PlayerPrefs.GetFloat("ScrollSpeed"))
        {
            scrollSpeed = PlayerPrefs.GetFloat("ScrollSpeed");
        }

        scrollSpeed = Mathf.Clamp(scrollSpeed,1,20);
        if (Time.timeScale!=0)
        {
            if (Physics.Raycast(qPivot.position, (ePivot.position - qPivot.position), out stickHit, Vector3.Distance(qPivot.position, ePivot.position)))
            {
                if (stickHit.collider != null && stickHit.collider.tag != "Player")
                {
                    if (stickHit.collider.tag == "ouch")
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    else if (stickHit.collider.tag == "flip" && !flipped)
                    {
                        rotDir *= -1;
                        BGSPIN.dir *= -1;
                        flipped = true;
                    }
                    else if (stickHit.collider.tag == "energy")
                    {
                        //collect energy
                        if (Vector3.Magnitude(stickHit.collider.transform.localPosition) < .1f) {
                            Destroy(stickHit.collider.gameObject);
                        }
                    }
                }
                else
                {
                    flipped = false;
                }
            }

            stickLength += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
            stickLength = Mathf.Clamp(stickLength, 1.5f, 8f);



            speed = Mathf.Lerp(speed, setSpeed, Time.deltaTime * dashSlow);
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                speed = dashSpeed;
            }

            if (qIsPivot)
            {
                activePivot = ePivot;
                staticPivot = qPivot;
                //trail.position = ePivot.position;
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(ePivot.position)), Vector2.zero);
                inStage = hit.collider != null;

                CameraController.focus = qPivot;
                qPivot.parent = transform;
                ePivot.parent = qPivot;

                //ePivot.localPosition += new Vector3(Input.GetAxisRaw("Mouse ScrollWheel"), 0, 0)*Time.deltaTime * scrollSpeed;
                //ePivot.localPosition += new Vector3(Input.GetAxis("Vertical"), 0, 0) * Time.deltaTime * scrollSpeed;
                ePivot.localPosition = new Vector3(Mathf.Lerp(ePivot.localPosition.x, stickLength, Time.deltaTime * 10), 0, 0);

                if (ePivot.localPosition.x > 8) { ePivot.localPosition = new Vector3(8, 0, 0); }
                if (ePivot.localPosition.x < 1.5f) { ePivot.localPosition = new Vector3(1.5f, 0, 0); }

                qPivot.Rotate(0, 0, speed * Time.deltaTime * rotDir);
                if (Input.GetKeyDown(KeyCode.Mouse0) && inStage) { qIsPivot = false; ePivot.localPosition = new Vector3(stickLength, 0, 0); }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && !inStage) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }//restart. temp die 


            }
            else
            {
                activePivot = qPivot;
                staticPivot = ePivot;
                //trail.position = qPivot.position;
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(qPivot.position)), Vector2.zero);
                inStage = hit.collider != null;

                CameraController.focus = ePivot;
                ePivot.parent = transform;
                qPivot.parent = ePivot;

                //qPivot.localPosition -= new Vector3(Input.GetAxisRaw("Mouse ScrollWheel"),0,0)*Time.deltaTime * scrollSpeed;
                //qPivot.localPosition -= new Vector3(Input.GetAxisRaw("Vertical"),0,0)*Time.deltaTime * scrollSpeed;
                qPivot.localPosition = new Vector3(Mathf.Lerp(qPivot.localPosition.x, -stickLength, Time.deltaTime * 10), 0, 0);

                if (qPivot.localPosition.x < -8) { qPivot.localPosition = new Vector3(-8, 0, 0); }
                if (qPivot.localPosition.x > -1.5f) { qPivot.localPosition = new Vector3(-1.5f, 0, 0); }

                ePivot.Rotate(0, 0, speed * Time.deltaTime * rotDir);
                if (Input.GetKeyDown(KeyCode.Mouse0) && inStage) { qIsPivot = true; qPivot.localPosition = new Vector3(-stickLength, 0, 0); }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && !inStage) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }//restart. temp die
            }

            activeHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(activePivot.position)), Vector2.zero);
            if (activeHit.collider != null && activeHit.collider.tag == "SAND")
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    activeHit.collider.gameObject.GetComponent<SandScript>().destroy = true;
                }
            }

            staticHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(staticPivot.position)), Vector2.zero);
            if (staticHit.collider == null)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (activeHit.collider != null && activeHit.collider.tag == "GOAL" && Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (GameObject.Find("SceneChanger"))
                {
                    GameObject.Find("SceneChanger").GetComponent<SceneChanger>().changeIt = true;
                }
            }

            if (GameObject.Find("Enemies"))
            {
                foreach (EnemyKillScript e in GameObject.Find("Enemies").GetComponentsInChildren<EnemyKillScript>())
                {
                    if (Vector3.Distance(activePivot.position, e.transform.position) < .6f)
                    {
                        e.killMe = true;
                    }
                }
            }
            if (GameObject.Find("EnergySpawnPoints"))
            {
                foreach (EnergyScript e in GameObject.Find("EnergySpawnPoints").GetComponentsInChildren<EnergyScript>())
                {
                    if (Vector3.Distance(activePivot.position, e.transform.position) < .7f)
                    {
                        //collect energy
                        if (Vector3.Magnitude(e.transform.localPosition) < .1f) {
                            Destroy(e.gameObject);
                        }
                    }
                }
            }
            eText.up = Vector3.up;
            qText.up = Vector3.up;
        }
        }

    private void LateUpdate()
    {
        LR.SetPosition(0, qPivot.position);
        LR.SetPosition(1, ePivot.position);
    }
}
