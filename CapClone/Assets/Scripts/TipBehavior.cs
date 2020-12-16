using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TipBehavior : MonoBehaviour
{
    public bool blue; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            if ((other.gameObject.GetComponent<BulletBehavior>().blue && blue) || (!other.gameObject.GetComponent<BulletBehavior>().blue && !blue))
            {
                Destroy(other.gameObject);
            }
            else if ((other.gameObject.GetComponent<BulletBehavior>().blue && !blue) || (!other.gameObject.GetComponent<BulletBehavior>().blue && blue))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (other.gameObject.tag == "boss")
        {
            if ((other.gameObject.GetComponent<BossBehavior>().blueBullet && blue) || (!other.gameObject.GetComponent<BossBehavior>().blueBullet && !blue))
            {
                Destroy(other.gameObject);
            }
            else if ((!other.gameObject.GetComponent<BossBehavior>().blueBullet && blue) || (other.gameObject.GetComponent<BossBehavior>().blueBullet && !blue))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
