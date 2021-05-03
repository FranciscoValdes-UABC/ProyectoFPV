using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement1 : MonoBehaviour
{
    public float speed;
    public CampoGravitatorio[] camposGravitatorios;

    public float Vo;
    public float Angulo;
    public float g;
    public float peso;
    Vector2 P;
    Vector2 V;
    Vector2 A;
    float F;

    //Esta variable será el centro cuando se confirme que está en un planeta
    private Transform center;
    private int dentro = 0;


    void Start()
    {
        P = new Vector2(transform.position.x, transform.position.y);
        V = new Vector2(Vo * Mathf.Cos(Angulo), Vo * Mathf.Sin(Angulo));
        //A = new Vector2(0, -g);
        peso = 1;

        //Lista en donde se encuentran todos los planetas
        camposGravitatorios = FindObjectsOfType<CampoGravitatorio>();
    }

    void Update()
    {
        if (dentro == 0)
        {
            foreach (CampoGravitatorio campo in camposGravitatorios)
            {
                //Si se encuentra dentro del rango utiliza coordenadas polares
                if (Vector2.Distance(campo.transform.position, transform.position) < campo.size)
                {
                    dentro = 1;
                    g = campo.gravedad;
                    A = (campo.transform.position - transform.position).normalized;
                    center = campo.transform;
                }
            }

        }
        else if (Vector2.Distance(center.transform.position, transform.position) > center.GetComponent<CampoGravitatorio>().size)
        {
            P = transform.position;
            dentro = 0;
            g = 0;
        }

        if (dentro == 1)
        {
            //Conversion cartesianas a polares
            /*print("Dentro: X: " + transform.position.x + "Y: " + transform.position.y);
            print("Velocidad X: " + V.x + " Y: " + V.y);*/
            PlanetMovement();
        }
        else
        {
            V.y = V.y - g * Time.deltaTime;
            P.x = P.x + V.x * Time.deltaTime;
            P.y = P.y + V.y * Time.deltaTime - 0.5f*g*Mathf.Pow(Time.deltaTime, 2);
            transform.position = P;
            /*print("Fuera: X: " + transform.position.x + "Y: " + transform.position.y);
            print("Velocidad X: " + V.x + " Y: " + V.y);*/
        }
    }

    //Esta funcion se encarga del movimiento en general.
    void PlanetMovement()
    {
        A = (center.position - transform.position);
        F = g*((transform.localScale.x*center.localScale.x*60)/Mathf.Pow(A.magnitude, 2));
        print(F);
        V += (F * A.normalized * peso) * Time.deltaTime;
        Vector3 v3 = V;
        transform.position += v3 * Time.deltaTime;
    }
}
