using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovers : MonoBehaviour
{

    public StickPivots stick;

    public bool isQ;
    SpriteRenderer SR;
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stick.qIsPivot)
        {
            if (isQ)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1)) { transform.localScale = new Vector3(4f, .4f, 2f); }
                transform.up = transform.parent.right;
                SR.flipX = stick.qPivot.position.x > stick.ePivot.position.x;
            }
            else
            {
                transform.up = Vector3.up;
                SR.flipX = stick.qPivot.position.x < stick.ePivot.position.x;
            }
        }
        else
        {
            if (isQ)
            {
                transform.up = Vector3.up;
                SR.flipX = stick.qPivot.position.x > stick.ePivot.position.x;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse1)) { float stretch = stick.stickLength/2; stretch = Mathf.Clamp(stretch,0,4); transform.localScale = new Vector3(stretch, .4f, 2f); }
                transform.up = transform.parent.right;
                SR.flipX = stick.qPivot.position.x < stick.ePivot.position.x;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Mouse1)) { transform.localScale = new Vector3(2f,.4f,2f); }
        transform.localScale = Vector3.Lerp(transform.localScale,Vector3.one,Time.deltaTime * 10);

    }

    int frames;

    private void FixedUpdate()
    {
        frames++;

        if (frames%10==0)
        {
            if (stick.qIsPivot)
            {
                if (isQ)
                {
                    transform.localScale = new Vector3(1.2f,.6f,1.2f);
                }
            }
            else
            {
                if (!isQ)
                {
                    transform.localScale = new Vector3(1.2f, .6f, 1.2f);
                }
            }
        }
    }
}
