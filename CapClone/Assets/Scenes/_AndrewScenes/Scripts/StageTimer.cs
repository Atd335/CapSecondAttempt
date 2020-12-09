using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageTimer : MonoBehaviour
{
    public float timer;
    TextMeshPro timeText;
    int smush;
    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TextMeshPro>();   
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer>=smush)
        {
            //transform.localScale = new Vector3(1.5f,.3f,0);
            smush++;
        }
        timeText.text = (Mathf.Floor((timer*100))/100).ToString();

        transform.localScale = Vector3.Lerp(transform.localScale,Vector3.one,Time.deltaTime * 15);
    }
}
