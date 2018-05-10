using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour {

    //
    public Text[] text;
    private int TextNum;

	// Use this for initialization
	void Start () {

        //表示するログの数
        TextNum = text.Length;

        //ログを空にする
        Clear();

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A)) AddLog("Script: Stage->stage.cs 次のエリアが解放された");
        if (Input.GetKeyDown(KeyCode.S)) AddLog("Script: Player->hoge.cs 攻撃");
        if (Input.GetKeyDown(KeyCode.D)) AddLog("Script: Player->Col.cs 敵の攻撃にあたった");
        if (Input.GetKeyDown(KeyCode.F)) AddLog("Script: Player->hoge.cs 敵を倒した");

    }

    //nのログをn+1にコピーする
    void MoveLog()
    {

        for (int i = TextNum - 1; i >= 1; i--)
        {
            text[i].text = "" + text[i-1].text;
        }

    }

    public void AddLog( string moji )
    {

        MoveLog();
        text[0].text = "" + moji;
    }


    public void Clear()
    {

        for (int i = 0; i < TextNum; i++)
        {
            text[i].text = "";
        }

    }
}

static public class LogManager
{
    static private GameObject Instance;
    static public void Init() { Instance = GameObject.FindWithTag("GameLog"); }
    static public void Update()
    {

    }
    //オンオフ(表示&非表示)の切り替え
    static public void Toggle() { Instance.SetActive(!Instance.active); }
    //ログを消す
    static public void Clear() { Instance.GetComponent<Log>().Clear(); }

    //文字を入れる
    static public void AddLog(string log) { Instance.GetComponent<Log>().AddLog(log); }
}