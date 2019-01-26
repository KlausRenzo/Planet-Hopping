using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    private float speed;

    void Update()
    {
        GameManager.Instance.vertexIndex = Mathf.FloorToInt(speed);
        speed += Input.GetAxis("Horizontal") * Time.deltaTime * 10f / GameManager.Instance.VertexDistance();
        speed = (speed + GameManager.Instance.Vertexes.Count) % GameManager.Instance.Vertexes.Count;
        transform.position = Vector3.Lerp(GameManager.Instance.CurrentVertexPosition,
            GameManager.Instance.NextVertexPosition, speed % 1);
    }
}