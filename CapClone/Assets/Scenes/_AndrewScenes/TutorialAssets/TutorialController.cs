using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public int tutorialStage;
    public Sprite[] mouseSprites;
    SpriteRenderer SR;

    bool switchSprite;

    TextMeshPro text;
    public GameObject[] newHalls;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInParent<TextMeshPro>();
        SR = GetComponent<SpriteRenderer>();
        tutorialStage = 1;
    }
    bool tutDone;
    // Update is called once per frame
    void Update()
    {
        if (switchSprite)
        {
            SR.sprite = mouseSprites[tutorialStage];
        }
        else
        {
            SR.sprite = mouseSprites[0];
        }


        if (tutorialStage == 1)
        {
            text.text = "MOVE";
            if (Input.GetKeyDown(KeyCode.Mouse0)) { tutorialStage++; text.color = new Color(text.color.r, text.color.g, text.color.b, 0); }
        }
        else if (tutorialStage == 2)
        {
            text.text = "STRETCH";
            if (Input.GetAxis("Mouse ScrollWheel") != 0) { tutorialStage++; text.color = new Color(text.color.r, text.color.g, text.color.b, 0); }
        }
        else if (tutorialStage == 3)
        {
            text.text = "DASH";
            if (Input.GetKeyDown(KeyCode.Mouse1)) { tutDone = true; }
        }

        if (tutDone)
        {
            transform.parent.position += new Vector3(0, 1, 0)*Time.deltaTime * 30;
            if (transform.position.y>20)
            {
                newHalls[0].SetActive(true);
                newHalls[1].SetActive(true);
                newHalls[2].SetActive(true);
                Destroy(this.gameObject);
            }
        }

        text.color = Color.Lerp(text.color, new Color(text.color.r, text.color.g, text.color.b,1),Time.deltaTime * 10);
    }

    int frames;
    public int speed;

    private void FixedUpdate()
    {
        frames++;
        if (frames%speed == 0)
        {
            if (!switchSprite)
            {
                transform.localScale = new Vector3(1.2f,.6f,0);
            }
            switchSprite = !switchSprite;
        }
    }
}
