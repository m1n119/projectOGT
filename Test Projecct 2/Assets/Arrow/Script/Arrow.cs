using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private const float speed = 0.1f;
    public float power = 1.0f;

    public float movep = 0.0f;
    public float addmove = 2.0f;

    float x;
    float y;
    Vector3 AxisPos = new Vector3(0, 0, 0);
    Vector3 dir = new Vector3(0, 0, 0);
    Vector3 EndPos = new Vector3(0, 0, 0);

    int state = 0;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        //string s = "" + Time.deltaTime;
        //TelopManager.Set(s);

        switch (state)
        {

            case 0:　//入力待ち
                AxisPos.x = Input.GetAxis("Horizontal");
                AxisPos.y = Input.GetAxis("Vertical");
                Vector3 Position = transform.position;
                Position.x += AxisPos.x;
                Position.y += AxisPos.y;
                dir = (Position - transform.position).normalized;

                transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
                //transform.position += new Vector3(x*speed, y*speed, 0);
                if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Move"))
                {
                    movep = addmove;
                    state++;

                }
                break;

            case 1: //準備

                EndPos = transform.position + dir * power;
                state++;
                break;

            case 2: //実行(移動)
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir * 10, 0.1f);
                Debug.DrawRay(transform.position, dir * 4, Color.blue, 1);
                if( hit.collider != null && hit.transform.tag == "Shima")
                {
                    hit.collider.gameObject.GetComponent<Shima>().Fire();
                }

                float foo = Vector3.Distance(transform.position, EndPos);
                if( foo < 0.5f ) state = 0;
                transform.position += dir / 3.0f;

                break;
        }



    
    }
     
    

    IEnumerator MoveCoroutine( Vector3 d, int foo )
    {
        float p = 0.5f;
        for (int i = foo; 0 < i; i--)
        {
            transform.position += dir * ( ( i / 10 ) );
            p -= 0.1f;
            yield return null;
        }
        state = 0;
        Debug.Log("STATE:" + state);
        yield return null;
    } 
}
