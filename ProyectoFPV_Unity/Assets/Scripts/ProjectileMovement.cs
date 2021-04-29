using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed;
    public float gravedad;
    public CampoGravitatorio[] camposGravitatorios;

    //Este representa la posicion en valores polares
    private Vector2 pos;

    //Esta variable será el centro cuando se confirme que está en un planeta
    private Transform center;
    private int dentro = 0;


    void Start()
    {
        pos = transform.position;

        //Lista en donde se encuentran todos los planetas
        camposGravitatorios = FindObjectsOfType<CampoGravitatorio>();
    }

    void Update()
    {
        //Aqui verificamos si el usuario esta haciendo algun input (ya sea horizontal o vertical), si es asi entonces
        //se manda a la funcion correspondiente el signo del movimiento (Esto puede ser interpretado como si el jugador se quiere mover
        //hacia enfrente o hacia atras)

        if (dentro == 0)
        {
            foreach (CampoGravitatorio campo in camposGravitatorios)
            {
                //Si se encuentra dentro del rango utiliza coordenadas polares
                if (Vector2.Distance(campo.transform.position, transform.position) < campo.size)
                {
                    center = campo.transform;
                    dentro = 1;
                    pos.x = Mathf.Sqrt(Mathf.Pow((transform.position.x - center.position.x), 2) + Mathf.Pow((transform.position.y - center.position.y), 2));
                    pos.y = Mathf.Atan2((transform.position.x - center.position.x) , (transform.position.y - center.position.y));
                }
            }

        }else if (Vector2.Distance(center.transform.position, transform.position) > center.GetComponent<CampoGravitatorio>().size){
                dentro = 0;
                transform.position = (Vector2)center.position + new Vector2(pos.x * Mathf.Sin(pos.y), pos.x * Mathf.Cos(pos.y));          
        }

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if(dentro == 1)
            {
                //Conversion cartesianas a polares
                print("Dentro: X: " + transform.position.x + "Y: " + transform.position.y);
                GeneralPolarMovement();
            } 
            else{
                transform.position = new Vector2(transform.position.x + Time.deltaTime * (speed) * Input.GetAxis("Horizontal"), transform.position.y + Time.deltaTime * (speed) * Input.GetAxis("Vertical"));
                print("Fuera: X: " + transform.position.x + "Y: " + transform.position.y);
            }
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
        pos.y += Time.deltaTime * (speed) * Input.GetAxis("Horizontal");
        //pos.x += Time.deltaTime * (speed) * Input.GetAxis("Vertical");
        pos.x += Time.deltaTime * (speed) - (center.GetComponent<CampoGravitatorio>().gravedad * 0.01f);

        //Despues de aumentar dicho angulo aplico la funcion de coordenadas polares
        transform.position = (Vector2)center.position + new Vector2(pos.x * Mathf.Sin(pos.y), pos.x * Mathf.Cos(pos.y));
    }
}
