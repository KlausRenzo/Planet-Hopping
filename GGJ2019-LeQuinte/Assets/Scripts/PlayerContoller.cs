using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerContoller : MonoBehaviour
{
    public float speed;
    [Range(0,10)]
    public float GeometrySpeedModifier;
    [Range(0, 1)]
    public float RotationSpeed = 0.2f;

    [Range(0, 50)] public float CircleSpeedModifier;



    //void Update()
    //{
    //    //speed += - Input.GetAxis("Horizontal") * Time.deltaTime * GeometrySpeedModifier / VertexController.Instance.VertexDistance();
    //    //speed = (speed + VertexController.Instance.Vertexes.Count) % VertexController.Instance.Vertexes.Count;
    //    //VertexController.Instance.vertexIndex = Mathf.FloorToInt(speed);

    //    //transform.position = Vector3.Lerp(VertexController.Instance.CurrentVertexPosition, VertexController.Instance.NextVertexPosition, speed % 1);

    //    //transform.up = Vector3.Lerp(transform.up, -VertexController.Instance.NormalVector.normalized, RotationSpeed);
    //    ////transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(VertexController.Instance.SideVector), 0.1f);
    //    ////Vector3 v = transform.rotation.eulerAngles;

    //    ////transform.rotation = Quaternion.Euler(0,0, v.z);

    //}
}