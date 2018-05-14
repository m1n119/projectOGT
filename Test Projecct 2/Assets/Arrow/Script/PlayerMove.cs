using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    //振動
    public GameObject XInputDotNet;
    private baibu Baibu;

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
        Move,
        Charge
    }
  
    public float GetMaxSpeed() { return MaxSpeed; }
    public float GetSpeed() { return Speed; }

    private static bool inst = false;
    void Awake()
    {
        if (!inst)
        {
            DontDestroyOnLoad(this.gameObject);
            inst = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }
    // Use this for initialization
    void Start ()
    {

        shake = GameObject.FindWithTag("MainCamera").GetComponent<ShakeCamera>();
        Baibu = XInputDotNet.GetComponent<baibu>();

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

                //滑り止め
                Baibu.SetPower(0.0f);
                Speed = 0.0f;
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                shake.Stop();
                

                //ボタン入力でチャージ状態にしてからチャージを開始
                if (Input.GetButtonDown("Move"))
                {
                    State = (int)state.Charge;
                    if (!ChargeFlg) StartCoroutine(Charge());
                }

                break;

            case (int)state.Move:

                //移動中
                if (!ChargeFlg)
                {
                    //レイ
                    //RaycastHit2D hit = Physics2D.Raycast(transform.position, dir * 10, 0.1f);
                    //Debug.DrawRay(transform.position, dir * 4, Color.blue, 1);
 

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
                    GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    Speed = 0.0f;
                    State = (int)state.Charge;
                    StartCoroutine(Charge());
                }

                break;

            case (int)state.Charge:

                //とまったとき
                if (Speed < 0) State = (int)state.Initialize;

                //チャージ中の処理
                if (!ChargeFlg && Input.GetButtonDown("Move"))
                {
                    Speed = 0f;
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
            Baibu.SetPower(0.2f);
            shake.SetShake(0.3f);
            Speed++;
            if (Speed > MaxSpeed) Speed = MaxSpeed;
            if (Input.GetButtonUp("Move"))
            {
                Baibu.SetPower(0);
                break;
            }
            
            yield return null;
        }

        Baibu.SetPower(1.0f);
        State = (int)state.Move;
        shake.SetShake(3.0f);
        yield return new WaitForSeconds(0.15f);
        ChargeFlg = false;
        shake.SetShake(0.1f);
        Baibu.SetPower(0.03f);
        yield return null;

    }   
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.transform.tag == "Small")
        {
            if (!c.GetComponent<Small>().SmallFire) {

                c.GetComponent<Small>().SmallFire = true;
                //shake.ShakeShakeShake(1.0f, 3.0f);
                Baibu.Play(1.0f, 0.1f);
            }
    
        }
    }


}
