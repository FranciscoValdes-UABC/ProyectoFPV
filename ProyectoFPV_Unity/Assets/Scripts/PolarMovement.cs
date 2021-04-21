using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarMovement : MonoBehaviour
{
    public Transform center;
    public float speed;
    private Vector2 pos;


   
    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        //Aqui verificamos si el usuario esta haciendo algun input (ya sea horizontal o vertical), si es asi entonces
        //se manda a la funcion correspondiente el signo del movimiento (Esto puede ser interpretado como si el jugador se quiere mover
        //hacia enfrente o hacia atras)
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            horizontalPolarMovement((int)Mathf.Sign(Input.GetAxis("Horizontal")));
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            verticalPolarMovement((int)Mathf.Sign(Input.GetAxis("Vertical")));
        }
    }

    //Esta funcion se encarga del movimiento "Horizontal", como este se hace de forma diferente que el movimiento vertical decidi hacerlo en dos funciones.
    void horizontalPolarMovement(int sign){

        //Esta parte del codigo se encarga de hacer que la rotacion del objeto concorde con su posicion relativa al centro.
        //Primero se obtiene el vector de direccion que apunte del objeto al centro
        Vector3 targetDirection = center.position - transform.position;
        //Como el juego es 2D la z se descarta
        targetDirection.z = 0;
        //Se aplica la rotacion con la funcion  Quaternion.LookRotation
        transform.rotation = Quaternion.LookRotation(targetDirection);

        //pos.x += Time.deltaTime;
        pos.y += Time.deltaTime * (speed * sign);
        transform.position = (Vector2)center.position + new Vector2(pos.x * Mathf.Sin(pos.y), pos.x * Mathf.Cos(pos.y));
    }

    void verticalPolarMovement(int sign)
    {

        pos.x += Time.deltaTime * (speed * sign);
        transform.position = (Vector2)center.position + new Vector2(pos.x * Mathf.Sin(pos.y), pos.x * Mathf.Cos(pos.y));
    }
}
