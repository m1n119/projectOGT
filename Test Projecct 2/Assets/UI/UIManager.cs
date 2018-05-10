using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
    //プレイヤー関連
    private GameObject Player;
    private PlayerMove PlayerScript;
    private Slider PlayerChargeSlider;
    //ログ
    private GameObject GameLog;


	// Use this for initialization
	void Start ()
    {
        LogManager.Init();
        GameLog = GameObject.FindWithTag("GameLog");

        //プレイヤー関連
        Player = GameObject.FindWithTag("Player");
        PlayerChargeSlider = GameObject.Find("PlayerChargeSlider").GetComponent<Slider>();
        PlayerScript = Player.GetComponent<PlayerMove>();
        PlayerChargeSlider.maxValue = PlayerScript.GetMaxSpeed();
        
	}
	
	// Update is called once per frame
	void Update () {
        
        //ログ
        if (Input.GetKeyDown(KeyCode.O))
        {
            LogManager.Toggle();
        }


        //プレイヤー関連
        PlayerChargeSlider.value = PlayerScript.GetSpeed();

    }
}
