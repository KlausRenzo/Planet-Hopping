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

}
