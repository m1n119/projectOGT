using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 小島
public class Small : MonoBehaviour
{
    public bool SmallFire;
    float changeRed = 1.0f;
    float changeGreen = 1.0f;
    float changeBlue = 1.0f;
    float chageAlpha = 1.0f;

    public GameObject FireEffect;
    public GameObject Fire;
    public GameObject PlusEffect;
    public GameObject IrandFireEffect;


    private CircleCollider2D Cc2d;

    GameObject TemperatureCheck;    // 温度計
    public float 温度加算数;

    float HitStop;
    public bool CameraZoom;
    // Use this for initialization
    void Start()
    {
        SmallFire = false;
        Cc2d = GetComponent<CircleCollider2D>();
        TemperatureCheck = GameObject.Find("Slider");
        CameraZoom = false;
        HitStop = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (SmallFire)
        {
            changeGreen = 0.0f;
            changeBlue = 0.0f;
            Cc2d.radius = 4.0f;
        }
        if (Time.timeScale <= 0.0f)
        {
            HitStop++;
            CameraZoom = true;
        }
        else HitStop = 0;
        if (HitStop > 8) CameraZoom = false;
        if (HitStop > 10)
        {
            Time.timeScale = 1.0f;
            HitStop = 0;
        }

        this.GetComponent<SpriteRenderer>().color = new Color(changeRed, changeGreen, changeBlue, chageAlpha);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !SmallFire)
        {
            Time.timeScale = 0.0f;
            TemperatureCheck.GetComponent<Temperature>().TemperatureNum += 温度加算数;
            FindObjectOfType<TemperatureNum>().AddPoint(温度加算数);

            GameObject ef = Instantiate(PlusEffect, transform.position, Quaternion.identity) as GameObject;
            Destroy(ef, 0.37f);
            GameObject ef5 = Instantiate(IrandFireEffect, transform.position, Quaternion.identity) as GameObject;
            Destroy(ef5, 0.3f);
            GameObject ef2 = Instantiate(FireEffect, transform.position, Quaternion.identity) as GameObject;
            GameObject ef3 = Instantiate(Fire, transform.position, Quaternion.identity) as GameObject;

            //2018/05/14 編集(OGT)
            // SmallFire = true;
            // 2018/05/10 00:37 追加(OGT)
            LogManager.AddLog(this.name + "が燃えました");
        }
    }
}
