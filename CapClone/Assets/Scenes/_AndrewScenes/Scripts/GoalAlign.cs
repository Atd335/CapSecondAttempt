﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAlign : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Awake()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,-.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}