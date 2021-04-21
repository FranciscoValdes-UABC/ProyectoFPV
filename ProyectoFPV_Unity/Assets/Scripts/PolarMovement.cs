using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarMovement : MonoBehaviour
{
    public Transform center;
    public float speed;

    //Este representa la posicion en valores polares
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
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            GeneralPolarMovement( );
        }
       
    }

    //Esta funcion se encarga del movimiento en general.
    void GeneralPolarMovement()
    {

        //Esta parte del codigo se encarga de hacer que la rotacion del objeto concorde con su posicion relativa al centro.
        //Primero se obtiene el vector de direccion que apunte del objeto al centro
        Vector3 targetDirection = center.position - transform.position;
        //Como el juego es 2D la z se descarta
        targetDirection.z = 0;
        //Se aplica la rotacion con la funcion  Quaternion.LookRotation
        transform.rotation = Quaternion.LookRotation(targetDirection);

        //Como los valores de la variable "pos" representan el vector en coordenadas polares, para poder moverme "Horizontalmente" alrededor del centro
        //Tengo que aumentar el valor del angulo, en este caso sera el valor representado por "y" dentro del vector
        //Ademas para poder moverme "Verticalmente" respecto del centro
        //Tengo que aumentar el valor del radio, en este caso sera el valor representado por "x" dentro del vector
        pos.y += Time.deltaTime * (speed)* Input.GetAxis("Horizontal");
        pos.x += Time.deltaTime * (speed) * Input.GetAxis("Vertical");
        //Despues de aumentar dicho angulo aplico la funcion de coordenadas polares
        transform.position = (Vector2)center.position + new Vector2(pos.x * Mathf.Sin(pos.y), pos.x * Mathf.Cos(pos.y));
    }

}
