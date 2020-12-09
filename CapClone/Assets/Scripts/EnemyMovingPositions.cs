using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingPositions : MonoBehaviour
{
    public List<Transform> locations;
    public Transform currentTarget;
    public int locationNumber;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = locations[0];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, currentTarget.position, Time.deltaTime * speed);
        if (Vector3.Distance(this.transform.position, currentTarget.position) < .2f)
        {
            locationNumber++;
            if (locationNumber == locations.Count)
            {
                locationNumber = 0;
            }
            currentTarget = locations[locationNumber];
        }
    }
}
