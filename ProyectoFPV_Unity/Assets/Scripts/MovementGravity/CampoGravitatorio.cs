using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoGravitatorio : MonoBehaviour
{

    /*Se guarda el tamaño del campo, su constante de gravedad y
     * el prefab de un objeto que funcionara como la representacion
     * visual del campo de gravedad.
    */
    public float size;
    public float gravedad;
    public GameObject campo;


    //Esta funcion ocurre en el momento en el que inicia el juego.
    void Start()
    {
        /*Las siguientes lineas de codigo instancian el objeto que servira como la representacion visual del campo de gravedad
         * le da una rotacion y posicion para posteriormente agregar una escala que concuerde con el tamaño especificado.
        */
        GameObject pro = Instantiate(campo, transform.position, Quaternion.Euler(90,90,90)) as GameObject;      
        pro.transform.localScale = new Vector3(size * 2, size * 2, size * 2);
        pro.transform.parent = transform;
    }

    //Esta funcion dibujara un circulo azul del tamaño del campo de gravedad cada vez que este sea seleccionado
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, size);

    }
}
