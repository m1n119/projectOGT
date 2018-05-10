using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shima : MonoBehaviour {

    //
    private Vector3 pos;
    public Vector3 GetPos(){ return pos; }

    private int State;
    enum state
    {
        Normal = 0, //普通の状態
        Fire = 1    //燃えてる状態
    }


	// Use this for initialization
	void Start ()
    {
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void Fire()
    {
        if ( State == (int)state.Fire ) return;
        State = (int)state.Fire;
        GetComponent<SpriteRenderer>().color = new Color( 256, 0, 0, 256 );
        LogManager.AddLog(this.name + "が燃えました");
    }
  

}
