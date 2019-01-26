using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    [Range(0, 10)]
    public float Radius;

    public PlayerContoller Player;
    public Vector3 StartingPosition;

    private float angle;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, Radius);
    }

    void Start()
    {
        Player.transform.position = Vector3.up * Radius;
    }
    void Update()
    {
        angle = Input.GetAxis("Horizontal") * Time.deltaTime * Player.CircleSpeedModifier;
        Player.transform.RotateAround(Vector3.zero, Vector3.back, angle);
    }
}
