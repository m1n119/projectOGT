using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private Transform target;
    public Vector3 offset = Vector3.zero;
   	// Use this for initialization
	void Start () {
        target = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = target.transform.position + offset;
    }

}
