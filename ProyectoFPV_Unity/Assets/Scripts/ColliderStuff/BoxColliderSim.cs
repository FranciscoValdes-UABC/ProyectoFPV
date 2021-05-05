using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderSim : MonoBehaviour
{
    //Este script simulara lo que realiza el box collider de unity
    //Contenera un arreglo de 2 valores flotantes:
    //    0 - Altura
    //    1 - Anchura

    public float[] sizes = new float[2];

    void Start()
    {
        sizes[0] = this.transform.localScale.x;
        sizes[1] = this.transform.localScale.y;
    }

    void OnDrawGizmosSelected()
    {
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = rotationMatrix;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(sizes[0], sizes[1], 0));
    }
}
