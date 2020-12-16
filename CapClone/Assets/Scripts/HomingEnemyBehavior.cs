using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingEnemyBehavior : MonoBehaviour
{
    public StickPivots player;
    public Transform playerPivot;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Stick").GetComponent<StickPivots>();
        if (player.qIsPivot)
        {
            playerPivot = player.qPivot;
        }
        else
        {
            playerPivot = player.ePivot;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.qIsPivot)
        {
            playerPivot = player.qPivot;
        }
        else
        {
            playerPivot = player.ePivot;
        }
        if (Mathf.Abs(Vector3.Distance(playerPivot.position, this.transform.position)) >= 20f)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPivot.position, 0.1f);
        }
        else if (Mathf.Abs(Vector3.Distance(playerPivot.position, this.transform.position)) >= 10f)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPivot.position, 0.05f);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPivot.position, 0.04f);
        }
    }
}
