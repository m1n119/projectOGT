using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour
{
    public float setShakeTIme; // 持続振動時間
    private float lifeTime;
    private Vector3 savePosition;
    private Vector3 offset;
    private float lowRangeX;
    private float maxRangeX;
    private float lowRangeY;
    private float maxRangeY;
    private float Zjiku;
    static public bool ShakeFlg = false;
    public bool GetShakeFlg() { return ShakeFlg; }
    private float timer;

    void Start()
    {
        if (setShakeTIme <= 0.0f)
            setShakeTIme = 0.2f;
        lifeTime = 0.0f;
        ShakeFlg = false;
        offset = GetComponent<CameraScript>().offset;
    }

    void Update()
    {
        if (lifeTime <= 0.0f)
        {
            timer = 0;
            offset.z = -1;
            GetComponent<CameraScript>().offset = offset;
            lifeTime = 0.0f;
            ShakeFlg = false;
        }

        if (lifeTime > 0.0f)
        {
            timer++;
            ShakeFlg = true;
            lifeTime -= Time.deltaTime;
            float x_val = Random.Range(lowRangeX, maxRangeX);
            float y_val = Random.Range(lowRangeY, maxRangeY);
            //transform.position = new Vector3(x_val, y_val, transform.position.z);
            GetComponent<CameraScript>().offset = new Vector3(x_val, y_val, -1);
        }

        if (timer > 60)
        {
            lifeTime = 0.0f;
        }
        if (Input.GetKeyDown("space"))
        {
            if (ShakeFlg == false) CatchShake();
        }
    }

    public void CatchShake()
    {
        if (ShakeFlg == true) return;
        savePosition = offset;
        lowRangeY = savePosition.y - 0.1f;
        maxRangeY = savePosition.y + 0.1f;
        lowRangeX = savePosition.x - 0.1f;
        maxRangeX = savePosition.x + 0.1f;
        lifeTime = setShakeTIme;
    }
}