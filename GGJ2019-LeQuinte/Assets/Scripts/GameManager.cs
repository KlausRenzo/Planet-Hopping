using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public List<Vertex> Vertexes = new List<Vertex>();

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
        for (int i = 0; i < Vertexes.Count; i++)
        {
            Gizmos.DrawLine(Vertexes[i].transform.position, Vertexes[i + 1]?.transform.position);
        }
    }
}