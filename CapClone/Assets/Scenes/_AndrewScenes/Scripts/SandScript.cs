using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandScript : MonoBehaviour
{
    public bool destroy;
    public Color thisColor;

    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        thisColor = GetComponent<SpriteRenderer>().color;
        destroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroy)
        {
            thisColor.a -= Time.deltaTime * speed;
        }
        thisColor.a = Mathf.Clamp(thisColor.a,0,1);
        GetComponent<SpriteRenderer>().color = thisColor;
        GetComponent<Collider2D>().enabled = thisColor.a > 0;
    }
}
