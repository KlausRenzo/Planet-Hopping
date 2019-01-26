using System;
using System.Collections.Generic;
using UnityEngine;

public class VertexController : MonoBehaviour
{
    public static VertexController Instance = null;
    public List<Vertex> Vertexes = new List<Vertex>();
    public int vertexIndex = 0;
    private int nextVertexIndex => vertexIndex.Next();
    public Vector3 SideVector => CalculateSideVector();
    public Vector3 NormalVector => CalculateNormalVector();
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
        if (Application.isEditor)
        {
            return;
        }

        for (int i = 0; i < Vertexes.Count; i++)
        {
            Gizmos.DrawLine(Vertexes[i].transform.position, Vertexes[i.Next()].transform.position);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Player.transform.position, Player.transform.position + NormalVector.normalized);
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

    private Vector3 CalculateNormalVector()
    {
        Vector3 side = CalculateSideVector();
        return new Vector3(side.y, -side.x,0);
    }

    public float VertexDistance()
    {
        return (Vertexes[vertexIndex].transform.position - Vertexes[vertexIndex.Next()].transform.position).magnitude;
    }

    public void Update()
    {
        Player.speed += -Input.GetAxis("Horizontal") * Time.deltaTime * Player.GeometrySpeedModifier / VertexDistance();
        Player.speed = (Player.speed + Vertexes.Count) % Vertexes.Count;
        vertexIndex = Mathf.FloorToInt(Player.speed);

        Player.transform.position = Vector3.Lerp(CurrentVertexPosition, NextVertexPosition, Player.speed % 1);

        Player.transform.up = Vector3.Lerp(Player.transform.up, -NormalVector.normalized, Player.RotationSpeed);
    }

}