using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoGravitatorio : MonoBehaviour
{
    //Este script simulara lo que realiza el circle collider de unity
    //Contenera un  valor flotante que simularan el radio del circulo

    public float size;
    public float gravedad;
    public GameObject campo;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, size);
        
    }

    void Start()
    {
        GameObject pro = Instantiate(campo, transform.position, Quaternion.Euler(90,90,90)) as GameObject;      
        pro.transform.localScale = new Vector3(size * 2, size * 2, size * 2);
        pro.transform.parent = transform;
    }
}
