using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SketchifySprite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        baseVector = Vector3.one;    
    }

    // Update is called once per frame
    void Update()
    {
        baseVector = Vector3.Lerp(baseVector,Vector3.one,Time.deltaTime * 10);
    }

    int frames;
    Vector3 baseVector;
    private void FixedUpdate()
    {
        frames++;
        if (frames%10==0)
        {
            transform.localScale = baseVector + new Vector3(Random.Range(-.1f,.1f), Random.Range(-.1f, .1f), 1);
        }
    }
}
