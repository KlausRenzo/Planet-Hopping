using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public PlayerMovement Player;

    void Start()
    {
    }

    void OnDestroy()
    {
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(gameObject.transform.position, 0.1f);
    }
}