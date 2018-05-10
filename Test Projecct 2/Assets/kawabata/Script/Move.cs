using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    Vector3 Pos;
    public float Speed;

    void Start()
    {
        Pos = transform.position;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Pos.x += x * Speed;
        Pos.y += y * Speed;

        transform.position = Pos;
    }
}
