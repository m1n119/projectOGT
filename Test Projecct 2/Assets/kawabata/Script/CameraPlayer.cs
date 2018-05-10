using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour {
    private GameObject player = null;
    public Vector3 offset = Vector3.zero;


    void Start()
    {
        player = GameObject.Find("Player_Fire");

        offset = transform.position - player.transform.position;
    }

    void Update()
    {

        Vector3 newPosition = transform.position;
        newPosition.x = player.transform.position.x + offset.x;
        newPosition.y = player.transform.position.y + offset.y;
        newPosition.z = player.transform.position.z + offset.z;
        transform.position = newPosition;

    }
}
