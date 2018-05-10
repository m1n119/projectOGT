using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject commentPrefab;
    public float interval;
    public Vector3 pos;
    private bool flg;
    private string t;

    void Awake()
    {
        TelopManager.Init();
    }
    IEnumerator Start()
    {
        t = "てすとえすておうそてすと";
        flg = true;
        while (flg)
        {
            
            transform.position =  pos;
            commentPrefab.GetComponent<Telop>().text = t;
            Instantiate(commentPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(interval);
        }
    }

    void Update()
    {
        
        if( Input.GetKeyDown( KeyCode.V)) { flg = false; }
        if (!flg && Input.GetKeyDown(KeyCode.B)) { StartCoroutine(Start()); }
        //if (Input.GetKeyDown(KeyCode.N)) { TelopManager.Set("aaaa" + Time.deltaTime); }


    }

}

static public class TelopManager
{
    static private GameObject Instance;
    static public void Init() { Instance = GameObject.FindWithTag("Telop"); }
    static public void Update()
    {

    }
    //オンオフ(表示&非表示)の切り替え
    static public void Toggle() { Instance.SetActive(!Instance.active); }

    //文字を入れる
    //static public void Set(string txt) { Instance.GetComponent<Spawner>().Set(txt); }
}
