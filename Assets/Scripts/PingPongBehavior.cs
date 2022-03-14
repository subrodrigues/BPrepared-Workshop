using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongBehavior : MonoBehaviour
{
    private readonly float speed = 0.01f;
    private readonly float delta = 1f;  //delta is the difference between min y to max y.
    private Vector3 initPos;
    
    private void Start() {
        initPos = transform.position;
    }

    void Update() {
        float y = initPos.y + Mathf.Sin(Time.time) * delta; 
        Vector3 pos = new Vector3(initPos.x, y, initPos.z);
        transform.position = pos;
    }
}
