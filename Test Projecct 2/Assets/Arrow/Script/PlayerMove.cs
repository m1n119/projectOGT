using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    //スピードの最大値
    public float MaxSpeed = 80.0f;

    //減速(摩擦的なやつ)
    public float 減速 = 1.0f;
    public bool  ChargeFlg = false;     //チャージしている間true
    private Vector3 dir = new Vector3( 0, 1, 0 ); 
    private float Speed = 0f;

    private ShakeCamera shake;

    private int State;
    private enum state : int
    {
        Initialize = 0,
        Move
    }
  
    public float GetMaxSpeed() { return MaxSpeed; }
    public float GetSpeed() { return Speed; }

    // Use this for initialization
    void Start ()
    {

        shake = GameObject.FindWithTag("MainCamera").GetComponent<ShakeCamera>();
	}
	
	// Update is called once per frame
	void Update () {

        //移動方向指定
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (x != 0 && y != 0)
        {
            dir = new Vector3(x, y, 0);
            dir.Normalize();
        }

        switch (State)
        {
            case (int)state.Initialize:

                shake.Stop();
                //move = 80;
                if (Input.GetButtonDown("Move"))
                {
                    if (!ChargeFlg) StartCoroutine(Charge());
                }

                break;

            case (int)state.Move:

                //移動中
                if (!ChargeFlg)
                {
                    //レイ
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, dir * 10, 0.1f);
                    Debug.DrawRay(transform.position, dir * 4, Color.blue, 1);
                    if (hit.collider != null && hit.transform.tag == "Shima")
                    {
                        hit.collider.gameObject.GetComponent<Shima>().Fire();
                    }

                    //移動
                    {
                        GetComponent<Rigidbody2D>().velocity = dir * Speed;
                        Speed -= 減速;
                    }
           
                }

                //とまったとき
                if (Speed < 0) State = (int)state.Initialize;

                //移動中にチャージ(キャラは止まる)
                if (!ChargeFlg &&Input.GetButtonDown("Move") )
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                    Speed = 0f;
                    State = (int)state.Initialize;
                    StartCoroutine(Charge());
                }

                break;
        }

        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

    }

    IEnumerator Charge()
    {
        ChargeFlg = true;
        Speed = 0;
        while (true)
        {
            shake.SetShake(0.5f);
            Speed++;
            if (Speed > MaxSpeed) Speed = MaxSpeed;
            if (Input.GetButtonUp("Move"))
            {
                break;
            }
            
            yield return null;
        }
        State = (int)state.Move;
        shake.SetShake(5.0f);
        yield return new WaitForSeconds(0.2f);
        ChargeFlg = false;
        shake.SetShake(0.1f);

        yield return null;

    }
}
