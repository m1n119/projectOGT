using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour
{
    private GameObject Player;
    private Vector3 savePosition;
    private Vector3 offset;
    private float Power = 0.0f;

    //
    private float lowRangeX;
    private float maxRangeX;
    private float lowRangeY;
    private float maxRangeY;

    //
    static public bool ShakeFlg = false;
   
    public Vector3 Shake() { return offset;  }
    public void SetShake( Vector3 pos, float p)
    {
        StartCoroutine( ShakeItOff(pos, p) );
    }

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        offset = Player.transform.position - transform.position;
        ShakeFlg = false;
        
    }

    void Update()
    {
        if (!ShakeFlg)
        {
            offset.z = -1;    
            ShakeFlg = false;
        }
        else
        {
            ShakeFlg = true;
            float x_val = Random.Range(lowRangeX, maxRangeX);
            float y_val = Random.Range(lowRangeY, maxRangeY);
            offset = new Vector3(x_val, y_val, -1);
        }
    }

    IEnumerator ShakeItOff( Vector3 pos,float p )
    {
        Power = p;
        savePosition = pos;
        ShakeFlg = true;
        while (true)
        {
            lowRangeY = savePosition.y - Power;
            maxRangeY = savePosition.y + Power;
            lowRangeX = savePosition.x - Power;
            maxRangeX = savePosition.x + Power;
            Power -= 0.1f;
            if (Power <= 0) break;
            yield return null;
        }
        ShakeFlg = false;
        yield return null;
    }
}