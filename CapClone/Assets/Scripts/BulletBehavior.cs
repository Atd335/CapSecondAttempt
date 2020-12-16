using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public bool blue;

    public Transform playerPivot;

    public float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        if (blue)
        {
            playerPivot = GameObject.Find("Q").transform;
        }
        else
        {
            playerPivot = GameObject.Find("E").transform;
        }
        transform.LookAt(playerPivot);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector3.Distance(playerPivot.position, this.transform.position)) >= 30f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed * 10f);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

    }
}
