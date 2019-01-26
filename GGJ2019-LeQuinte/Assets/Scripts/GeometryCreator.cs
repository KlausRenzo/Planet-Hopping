﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeometryCreator : MonoBehaviour
{
    public GameObject sprite;
    public List<Vector2> Points;

    private void Start()
    {
        Points = sprite.GetComponent<PolygonCollider2D>().points.ToList();
        float firstDistance = Points[0].magnitude;
        float averageDistance = 0;
        int counter = 0;
        foreach (Vector3 point in Points)
        {
            Debug.Log(($"Point = {point}, Magnitude = {point.magnitude} , firstDistance = {firstDistance} - delta = {Mathf.Abs(point.magnitude - firstDistance)}"));
            if (Mathf.Abs(point.magnitude - firstDistance) > 0.1f)
            {
                counter++;
            }

            averageDistance += point.magnitude;
        }
        Debug.Log($"Counter= {counter}");

        averageDistance = averageDistance / Points.Count;
        if (counter < Points.Count / 10)
        {
            Debug.Log("Cerchio");
            CircleController c = this.gameObject.AddComponent<CircleController>();
            //TODO: Better initialization of CircleController, with player find
            c.Player = GameObject.Find("player").GetComponent<PlayerContoller>();
            c.Radius = averageDistance;
            c.center = sprite.transform.position;

        }
        else
        {
            VertexController v = this.gameObject.AddComponent<VertexController>();
            //TODO: Better initialization of VertexController, with player find
            v.Player = GameObject.Find("player").GetComponent<PlayerContoller>();
            Debug.Log("Geometria");
            foreach (Vector3 point in Points)
            {
                GameObject g = new GameObject();
                g.transform.SetParent(sprite.transform);
                g.transform.position = point + sprite.transform.position;
                g.AddComponent<Vertex>();
            }

        }
    }
}