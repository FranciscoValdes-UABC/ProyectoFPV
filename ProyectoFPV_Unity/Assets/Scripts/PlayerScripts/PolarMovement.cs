using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarMovement : MonoBehaviour
{
    public float speed;
    public Transform planetaCentro;
    public float altura;

    //Este representa la posicion en valores polares
    private Vector2 pos;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        pos = transform.position;
        GeneralPolarMovement();
        
    }

    void Update()
    {
             GeneralPolarMovement();
    }

    //Esta funcion se encarga del movimiento en general.
    void GeneralPolarMovement()
    {

        transform.right = transform.position - planetaCentro.position;

        //Como los valores de la variable "pos" representan el vector en coordenadas polares, para poder moverme "Horizontalmente" alrededor del centro
        //Tengo que aumentar el valor del angulo, en este caso sera el valor representado por "y" dentro del vector
        //Ademas para poder moverme "Verticalmente" respecto del centro
        //Tengo que aumentar el valor del radio, en este caso sera el valor representado por "x" dentro del vector
        pos.y += Time.deltaTime * (speed) * Input.GetAxis("Horizontal");
        pos.x = altura;

        //Despues de aumentar dicho angulo aplico la funcion de coordenadas polares
        transform.position = (Vector2)planetaCentro.position + new Vector2(pos.x * Mathf.Sin(pos.y), pos.x * Mathf.Cos(pos.y));
    }

    public void OnCollision(GameObject coll)
    {
        if(coll.tag == "ProjectPla2" && this.tag == "Player1")
        {
            gameManager.Win();
            Destroy(this.gameObject);
        }
        else if(coll.tag == "ProjectPla1" && this.tag == "Player2")
        {
            gameManager.Win();
            Destroy(this.gameObject);
        }
    }
}
