using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class VertexController : MonoBehaviour
{
    public static VertexController Instance = null;
    public List<Vector3> Vertexes = new List<Vector3>();
    public int vertexIndex = 0;
    private int nextVertexIndex => vertexIndex.Next();
    public Vector3 SideVector => CalculateSideVector();
    public Vector3 NormalVector => CalculateNormalVector();
    public PlayerContoller Player;
    public Vector3 CurrentVertexPosition => Vertexes[vertexIndex];
    public Vector3 NextVertexPosition => Vertexes[vertexIndex.Next()];


    private float JumpTimeStart;
    

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
            //return;
        }

        for (int i = 0; i < Vertexes.Count; i++)
        {
            Gizmos.DrawLine(Vertexes[i], Vertexes[i.Next()]);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Player.transform.position, Player.transform.position + NormalVector.normalized);
    }

    void Start()
    {
        for (var i = 0; i < Vertexes.Count; i++)
        {
            Vector3 vertex = Vertexes[i];
            Vector3 nextVertex = Vertexes[i.Next()];
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
        return new Vector3(side.y, -side.x, 0);
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
        if (Input.GetKeyDown(KeyCode.W))
        {
            JumpTimeStart = Time.time;
            // STATUS PREPARING
            Player.GetComponent<Animator>().Play("Preparing");
            Player.CanJump = true;
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.S))
        {
            Player.GetComponent<Animator>().Play("Idle");
            Player.CanJump = false;
        }

        if (Player.CanJump && Input.GetKeyUp(KeyCode.W))
        {
            if (Time.time - JumpTimeStart > Player.TimeToMakeBigJump)
            {
                Player.GetComponent<Animator>().Play("PlanetHop");
            }
            else
            {
                Player.GetComponent<Animator>().Play("Jump");
            }
        }
    }
}