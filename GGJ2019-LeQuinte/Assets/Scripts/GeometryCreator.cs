using System.Collections.Generic;
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
        foreach (Vector2 point in Points)
        {
            if (Mathf.Abs(point.magnitude - firstDistance) < firstDistance / 10f)
            {
                counter++;
            }

            averageDistance += point.magnitude;
        }

        averageDistance = averageDistance / Points.Count;
        if (counter > Points.Count / 10)
        {
            Debug.Log("Cerchio");
            GameObject g = new GameObject();
            CircleController c = g.AddComponent<CircleController>();
            c.Radius = averageDistance;
        }
        else
        {
            Debug.Log("Geometria");
            foreach (Vector3 point in Points)
            {
                GameObject g = new GameObject();
                g.transform.position = point;
                g.AddComponent<Vertex>();
            }
        }
    }
}