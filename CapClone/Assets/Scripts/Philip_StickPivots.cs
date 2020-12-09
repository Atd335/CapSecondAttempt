using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Philip_StickPivots : MonoBehaviour
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

    // PHILIP EXCLUSIVE VARIABLES
    public float boostSpeed;
    public Philip_BeatBump cameraBeat;
    public float originalCamSize;
    public bool boosted = false;
    public float maximumStickLength = 8f;
    public List<GameObject> boosts;

    void Start()
    {
        LR = GetComponent<LineRenderer>();
        stickLength = 1.5f;
    }

    public float stickLength;
    RaycastHit stickHit;

    public float flipTime;
    float flipTimer;
    bool flipped;

    Transform activePivot;

    void Update()
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
                    speed = setSpeed;
                    maximumStickLength = 8f;
                    cameraBeat.camSize = originalCamSize;
                    if (boosted)
                    {
                        for (int i = 0; i < boosts.Count; i++)
                        {
                            boosts[i].SetActive(true);
                        }
                        boosted = false;
                    }
                }
                else if (stickHit.collider.tag == "energy")
                {
                    //collect energy
                    if (Vector3.Magnitude(stickHit.collider.transform.localPosition) < .1f)
                    {
                        Destroy(stickHit.collider.gameObject);
                    }
                }
                else if (stickHit.collider.tag == "boost")
                {
                    cameraBeat.camSize += 1f;
                    boosted = true;
                    speed += 30f;
                    boostSpeed = speed;
                    maximumStickLength += 2f;
                    boosts.Add(stickHit.collider.gameObject);
                    stickHit.collider.gameObject.SetActive(false);
                }
            }
            else
            {
                flipped = false;
            }
        }

        stickLength += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        stickLength = Mathf.Clamp(stickLength, 1.5f, maximumStickLength);

        LR.SetPosition(0, qPivot.position);
        LR.SetPosition(1, ePivot.position);

        if (!boosted)
        {
            originalCamSize = cameraBeat.camSize;
            speed = Mathf.Lerp(speed, setSpeed, Time.deltaTime * dashSlow);
        }
        else
        {
            speed = Mathf.Lerp(speed, boostSpeed, Time.deltaTime * dashSlow); ;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            speed = dashSpeed;
        }

        if (qIsPivot)
        {
            activePivot = ePivot;
            //trail.position = ePivot.position;
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(ePivot.position)), Vector2.zero);
            inStage = hit.collider != null;

            CameraController.focus = qPivot;
            qPivot.parent = transform;
            ePivot.parent = qPivot;

            //ePivot.localPosition += new Vector3(Input.GetAxisRaw("Mouse ScrollWheel"), 0, 0)*Time.deltaTime * scrollSpeed;
            //ePivot.localPosition += new Vector3(Input.GetAxis("Vertical"), 0, 0) * Time.deltaTime * scrollSpeed;
            ePivot.localPosition = new Vector3(Mathf.Lerp(ePivot.localPosition.x, stickLength, Time.deltaTime * 10), 0, 0);

            if (ePivot.localPosition.x > maximumStickLength) { ePivot.localPosition = new Vector3(8, 0, 0); }
            if (ePivot.localPosition.x < 1.5f) { ePivot.localPosition = new Vector3(1.5f, 0, 0); }

            qPivot.Rotate(0, 0, speed * Time.deltaTime * rotDir);
            if (Input.GetKeyDown(KeyCode.Mouse0) && inStage) { qIsPivot = false; ePivot.localPosition = new Vector3(stickLength, 0, 0); }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && !inStage) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }//restart. temp die 


        }
        else
        {
            activePivot = qPivot;
            //trail.position = qPivot.position;
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(qPivot.position)), Vector2.zero);
            inStage = hit.collider != null;

            CameraController.focus = ePivot;
            ePivot.parent = transform;
            qPivot.parent = ePivot;

            //qPivot.localPosition -= new Vector3(Input.GetAxisRaw("Mouse ScrollWheel"),0,0)*Time.deltaTime * scrollSpeed;
            //qPivot.localPosition -= new Vector3(Input.GetAxisRaw("Vertical"),0,0)*Time.deltaTime * scrollSpeed;
            qPivot.localPosition = new Vector3(Mathf.Lerp(qPivot.localPosition.x, -stickLength, Time.deltaTime * 10), 0, 0);

            if (qPivot.localPosition.x < -maximumStickLength) { qPivot.localPosition = new Vector3(-8, 0, 0); }
            if (qPivot.localPosition.x > -1.5f) { qPivot.localPosition = new Vector3(-1.5f, 0, 0); }

            ePivot.Rotate(0, 0, speed * Time.deltaTime * rotDir);
            if (Input.GetKeyDown(KeyCode.Mouse0) && inStage) { qIsPivot = true; qPivot.localPosition = new Vector3(-stickLength, 0, 0); }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && !inStage) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }//restart. temp die
        }


        foreach (EnemyKillScript e in GameObject.Find("Enemies").GetComponentsInChildren<EnemyKillScript>())
        {
            if (Vector3.Distance(activePivot.position, e.transform.position) < .6f)
            {
                e.killMe = true;
            }
        }

        foreach (EnergyScript e in GameObject.Find("EnergySpawnPoints").GetComponentsInChildren<EnergyScript>())
        {
            if (Vector3.Distance(activePivot.position, e.transform.position) < .7f)
            {
                //collect energy
                if (Vector3.Magnitude(e.transform.localPosition) < .1f)
                {
                    Destroy(e.gameObject);
                }
            }
        }

        eText.up = Vector3.up;
        qText.up = Vector3.up;
    }
}
