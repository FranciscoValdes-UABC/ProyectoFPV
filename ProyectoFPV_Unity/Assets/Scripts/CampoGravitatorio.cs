using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoGravitatorio : MonoBehaviour
{
    //Este script simulara lo que realiza el circle collider de unity
    //Contenera un  valor flotante que simularan el radio del circulo

    public float size;
    public float gravedad;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, size);
    }
}
