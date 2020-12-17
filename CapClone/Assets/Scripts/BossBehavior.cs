using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public GameObject bullet1;
    public GameObject bullet2;

    public Material blue;
    public Material red;

    public bool blueBullet;

    public float createBulletEvery_Seconds;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= createBulletEvery_Seconds)
        {
            timer = 0;
            if (blueBullet)
            {
                CreateBullet1();
                gameObject.GetComponent<Renderer>().material = red;
            }
            else
            {
                CreateBullet2();
                gameObject.GetComponent<Renderer>().material = blue;
            }
            blueBullet = !blueBullet;
        }
    }

    public void CreateBullet1()
    {
        GameObject bullet = Instantiate(bullet1, this.transform);
        bullet.GetComponent<BulletBehavior>().blue = true;
    }

    public void CreateBullet2()
    {
        GameObject bullet = Instantiate(bullet2, this.transform);
        bullet.GetComponent<BulletBehavior>().blue = false;
    }
}
