using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Vertex : MonoBehaviour 
{
    void Start()
    {
        GameManager.Instance.Vertexes.Add(this);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, 0.5f);
    }

}
