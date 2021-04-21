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


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(sizes[0], sizes[1], 0));
    }
}
