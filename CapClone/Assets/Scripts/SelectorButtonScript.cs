using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectorButtonScript : MonoBehaviour
{
    public int sceneToTravelTo;
    public bool levelunlocked;
    public Transform scaler;
    // Start is called before the first frame update
    void Start()
    {
        scaler = transform.parent.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelunlocked)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    scaler.localScale = Vector3.Lerp(scaler.localScale, Vector3.one * 1.3f, Time.deltaTime * 10);
                    if (Input.GetKeyDown(KeyCode.Mouse0)) { GameObject.Find("SceneChanger").GetComponent<SceneChanger>().nextScene = sceneToTravelTo; GameObject.Find("SceneChanger").GetComponent<SceneChanger>().changeIt = true; }
                }
                else
                {
                    scaler.localScale = Vector3.Lerp(scaler.localScale, Vector3.one * 1, Time.deltaTime * 10);
                }
            }
            else
            {
                scaler.localScale = Vector3.Lerp(scaler.localScale, Vector3.one * 1, Time.deltaTime * 10);
            }
        }
        else
        {
            GetComponentInParent<SketchifyItem>().enabled = false;
            GetComponent<SpriteRenderer>().color = Color.grey;
            GetComponentInChildren<TextMeshPro>().color = Color.grey;
        }
    }
}
