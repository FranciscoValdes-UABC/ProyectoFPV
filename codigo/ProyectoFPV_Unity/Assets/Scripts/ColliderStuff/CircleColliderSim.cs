using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleColliderSim : MonoBehaviour
{
    //Este script simulara lo que realiza el circle collider de unity
    //Contenera un  valor flotante que simularan el radio del circulo
    public float size;

    //Esta funcion dibuja el collider en pantalla
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, size);
    }
}
