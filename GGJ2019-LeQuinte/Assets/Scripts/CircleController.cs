using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    [Range(0, 10)]
    public float Radius;

    public Vector3 center;
    public PlayerContoller Player;
    public Vector3 StartingPosition;

    private float angle;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(center, Radius);
    }

    void Start()
    {
        Player.transform.position = center + Vector3.up * Radius;
    }
    void Update()
    {
        angle = Input.GetAxis("Horizontal") * Time.deltaTime * Player.CircleSpeedModifier;
        Player.transform.RotateAround(center, Vector3.back, angle);
    }
}
