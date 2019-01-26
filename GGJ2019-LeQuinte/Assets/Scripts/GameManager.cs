using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public List<Vertex> Vertexes = new List<Vertex>();
    public int vertexIndex = 0;
    private int nextVertexIndex => vertexIndex.Next();
    public Vector3 SideVector => CalculateSideVector();
    public PlayerContoller Player;
    public Vector3 CurrentVertexPosition => Vertexes[vertexIndex].transform.position;
    public Vector3 NextVertexPosition => Vertexes[vertexIndex.Next()].transform.position;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(Instance);
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < Vertexes.Count - 2; i++)
        {
            Vertex a = Vertexes[i];
            Vertex b = Vertexes[i.Next()];
            //Vertex b = (i + 1 == Vertexes.Count) ? Vertexes[0] : Vertexes[i + 1];
            Gizmos.DrawLine(a.transform.position, b.transform.position);
        }
    }

    void Start()
    {
        for (var i = 0; i < Vertexes.Count; i++)
        {
            Vertex vertex = Vertexes[i];
            Vertex nextVertex = Vertexes[i.Next()];
            vertex.transform.rotation = Quaternion.LookRotation(nextVertex.transform.position);
            BoxCollider box = vertex.gameObject.AddComponent<BoxCollider>();
            box.size = new Vector3(0.1f, CalculateSideVector(i).x, 0.1f);
            box.center = CalculateSideVector(i) * 0.5f;
        }

        Player.gameObject.transform.position = Vertexes[0].transform.position;
    }

    private Vector3 CalculateSideVector(int index)
    {
        Vector3 side = Vertexes[index].transform.position - Vertexes[index.Next()].transform.position;
        return side;
    }

    private Vector3 CalculateSideVector()
    {
        Vector3 side = Vertexes[vertexIndex].transform.position - Vertexes[nextVertexIndex].transform.position;
        return side;
    }

    public float VertexDistance()
    {
        return (Vertexes[vertexIndex].transform.position - Vertexes[vertexIndex.Next()].transform.position).magnitude;
    }



}