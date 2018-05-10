using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Telop : MonoBehaviour {
 
    public string text;
    public float speed;
 
    void Start()
    {
        transform.SetParent(GameObject.Find("TelopCanvas").transform);
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        GetComponent<Text>().text = text;
        Destroy(gameObject, 30f);
    }

    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }

    public void Set(string txt)
    {
        text = txt;
    }


}


