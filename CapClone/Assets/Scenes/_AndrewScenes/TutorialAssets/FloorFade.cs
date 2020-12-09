using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorFade : MonoBehaviour
{
    Color startColor;
    // Start is called before the first frame update
    void Awake()
    {
        startColor = GetComponent<SpriteRenderer>().color;
        startColor.a = 1;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color,startColor,Time.deltaTime * 8);
    }
}
