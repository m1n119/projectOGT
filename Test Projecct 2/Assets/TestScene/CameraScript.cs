using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private ShakeCamera sc;
    private Transform target;

   	// Use this for initialization
	void Start () {
        target = GameObject.FindWithTag("Player").transform;
        sc = GetComponent<ShakeCamera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = target.transform.position + sc.Shake();
        if (Input.GetKeyDown(KeyCode.Y)) sc.SetShake(target.transform.position, 3.0f);
    }

}
