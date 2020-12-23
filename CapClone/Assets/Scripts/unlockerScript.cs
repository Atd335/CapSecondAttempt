using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (SelectorButtonScript s in GetComponentsInChildren<SelectorButtonScript>())
        {
            if (PlayerPrefs.GetInt(s.sceneToTravelTo.ToString())==1)
            {
                s.levelunlocked = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
