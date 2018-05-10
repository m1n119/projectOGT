using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 小島
public class Small : MonoBehaviour {
    public bool SmallFire;
    float changeRed = 1.0f;
    float changeGreen = 1.0f;
    float changeBlue = 1.0f;
    float chageAlpha = 1.0f;

    public GameObject FireEffect;
    public GameObject PlusEffect;


    private CircleCollider2D Cc2d;

    GameObject TemperatureCheck;    // 温度計
    public float 温度加算数;

    // Use this for initialization
    void Start () {
        SmallFire = false;
        Cc2d = GetComponent<CircleCollider2D>();
        TemperatureCheck = GameObject.Find("Slider");
    }
	
	// Update is called once per frame
	void Update () {
        if (SmallFire)
        {
            changeGreen = 0.0f;
            changeBlue = 0.0f;
        }

        if (SmallFire)
        {
            Cc2d.radius = 4.0f;
            GameObject ef = Instantiate(FireEffect, transform.position, Quaternion.identity) as GameObject;
            Destroy(ef, 0.2f);
        }

        this.GetComponent<SpriteRenderer>().color = new Color(changeRed, changeGreen, changeBlue, chageAlpha);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"&&!SmallFire)
        {
            TemperatureCheck.GetComponent<Temperature>().TemperatureNum += 温度加算数;

            GameObject ef = Instantiate(PlusEffect, transform.position, Quaternion.identity) as GameObject;
            Destroy(ef, 0.37f);
            SmallFire = true;

            // 2018/05/10 00:37 追加(OGT)
            LogManager.AddLog(this.name + "が燃えました");
        }
    }
}
