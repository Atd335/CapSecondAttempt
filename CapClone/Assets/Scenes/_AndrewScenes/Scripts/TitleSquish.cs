using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSquish : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }
    public int beatNum;
    public int increment;
    // Update is called once per frame
    void Update()
    {
        if (BeatBump.songPosInBeats >= beatNum)
        {
            transform.localScale = new Vector3(1.2f,.6f,1f);
            beatNum+=increment;

        }
        transform.localScale = Vector3.Lerp(transform.localScale,new Vector3(1,1,1),Time.deltaTime*10);
    }
}
