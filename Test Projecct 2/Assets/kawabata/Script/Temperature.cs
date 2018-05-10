using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 温度計
public class Temperature : MonoBehaviour {
    Slider Temperature_Slider;
    public float TemperatureNum;// 温度数
    // Use this for initialization
    void Start () {
        Temperature_Slider = GameObject.Find("Slider").GetComponent<Slider>();
        TemperatureNum = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        Temperature_Slider.value = TemperatureNum;
	}
}
