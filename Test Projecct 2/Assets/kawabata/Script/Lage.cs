using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 大島&中島
public class Lage : MonoBehaviour {
    private List<GameObject> m_hitObjects = new List<GameObject>();
    public GameObject FireEffect;
    public GameObject PlusEffect;

    bool FireWaitFlg;           // 周りが何個か燃えて燃え移ったときに起動
    int FireWait;               // FireWaitFlgが起動したら++;
    public bool TrueFireFlg;    // 完全に燃えました(^ω^)ｵｯｵｯｵｯ

    public int 燃え移るまでの個数;
    public int 完全に燃え移るまでの時間;


    GameObject TemperatureCheck;    // 温度計
    public float 温度加算数;

    void Start () {
        FireWaitFlg = false;
        TrueFireFlg = false;
        FireWait = 0;
        TemperatureCheck = GameObject.Find("Slider");
    }

    void Update ()
    {
        if (FireWaitFlg) FireWait++;

        if (FireWait >= 完全に燃え移るまでの時間)
        {
            TrueFireFlg = true;
            GameObject ef = Instantiate(PlusEffect, transform.position, Quaternion.identity) as GameObject;
            Destroy(ef, 0.3f);
            TemperatureCheck.GetComponent<Temperature>().TemperatureNum += 温度加算数;
        }
        if (TrueFireFlg) // 完全に燃えとる
        {
            GameObject ef = Instantiate(FireEffect, transform.position, Quaternion.identity) as GameObject;
            Destroy(ef, 0.2f);
            FireWait = 0;
        }
        //Debug.Log(FireWait);
    }


    void FixedUpdate()
    {
        if (m_hitObjects.Count >= 燃え移るまでの個数)
        {
            FireWaitFlg = true;
            LogManager.AddLog(this.name + "が燃えました");
        }

        m_hitObjects.Clear();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Small")
        {
            // オブジェクトをリストに登録!!!
            m_hitObjects.Add(collision.gameObject);
        }
    }

}