using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarMovement : MonoBehaviour
{
    // Esta es la velocidad del personaje.
    public float speed;
    // Este es transform del planeta sobre el cual esta el personaje.
    public Transform planetaCentro;
    // Esta es la altura del personaje en relacion con el planeta.
    public float altura;

    // Este representa la posicion en valores polares
    private Vector2 pos;

    // Una relacion con el objeto que contiene el GameManager
    private GameManager gameManager;

    //Esto es referencia al objeto que representa visualmente al jugador y su respectivo control de animacion.
    private Animator animator;
    public GameObject playerSprite;

    //Esta funcion ocurre en el inicio del juego, antes que la funcion Start
    void Awake(){
        //Se asignan los valores correspondientes a las variables definidas anteriormente.
        animator = playerSprite.GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        pos = transform.position;
        //Se inicia rapidamente el movimiento del jugador para evitar errores visuales.
        GeneralPolarMovement();
        
    }

    void Update()
    {
        //Manda a llamar a la funcion que se encarga del movimiento en general
        GeneralPolarMovement();

        /* Esta serie de ifs se encargan de mostrar el movimiento visualmente en el juego
         * Rotacion:
         * Si el jugador esta presionando la tecla indicativa de un movimiento hacia la izquierda entonces la
         * se realiza una rotacion de acuerdo a esto, si es a la derecha se reinicia la rotacion.
         * Movimiento:
         * Si el jugador esta presionando cualquier tecla indicativa de un movimiento entonces se actualiza a verdadero
         * el booleano que controla la animacion de movimiento dentro del controllador de no ser asi se regresa a falso. 
         */
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") >= 0)
            {
                playerSprite.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                playerSprite.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
                animator.SetBool("Moving", true);
        }
        else {
            animator.SetBool("Moving", false);
        }
    }

    //Esta funcion se encarga del movimiento en general.
    void GeneralPolarMovement()
    {
        //Esto obtiene la direccion que deberia tomarse como "arriba" segun la posicion del jugador en el planeta
        //cuando esto se le asigna al transform.up el jugador rota conforme a esto.
        transform.up = transform.position - planetaCentro.position;

        //Como los valores de la variable "pos" representan el vector en coordenadas polares, para poder moverme "Horizontalmente" alrededor del centro
        //Tengo que aumentar el valor del angulo, en este caso sera el valor representado por "y" dentro del vector
        //Ademas para poder moverme "Verticalmente" respecto del centro
        //Tengo que aumentar el valor del radio, en este caso sera el valor representado por "x" dentro del vector
        pos.y += Time.deltaTime * (speed) * Input.GetAxis("Horizontal");
        pos.x = altura;

        //Despues de aumentar dicho angulo aplico la funcion de coordenadas polares
        transform.position = (Vector2)planetaCentro.position + new Vector2(pos.x * Mathf.Sin(pos.y), pos.x * Mathf.Cos(pos.y));
    }

    //Esta funcion se ejecuta cuando se detecta una colision
    public void OnCollision(GameObject coll)
    {
        //Si el proyectil del jugador 2 colisiona con el jugador 1 entonces el jugador 2 gana y viceversa.
        if(coll.tag == "ProjectPla2" && this.tag == "Player1")
        {
            gameManager.Win(2);
            Destroy(this.gameObject);
        }
        else if(coll.tag == "ProjectPla1" && this.tag == "Player2")
        {
            gameManager.Win(1);
            Destroy(this.gameObject);
        }
    }
}
