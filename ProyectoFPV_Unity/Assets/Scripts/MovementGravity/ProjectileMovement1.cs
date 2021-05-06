using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement1 : MonoBehaviour
{
    public float speed;
    public CampoGravitatorio[] camposGravitatorios;

    public float Vo;
    //Esto se referencia en el script disparo
    public float Angulo;
    public float g;
    public float peso;
    //Posicion, velocidad y angulo
    Vector2 P;
    Vector2 V;
    Vector2 A;
    float F;
    float time;

    //Esta variable será el centro cuando se confirme que está en un planeta
    private Transform center;
    private int dentro = 0;


    void Start()
    {
        P = new Vector2(transform.position.x, transform.position.y);
        V = new Vector2(Vo * Mathf.Cos(Angulo), Vo * Mathf.Sin(Angulo));
        peso = 1;

        //Lista en donde se encuentran todos los planetas
        camposGravitatorios = FindObjectsOfType<CampoGravitatorio>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (dentro == 0){
            foreach (CampoGravitatorio campo in camposGravitatorios) {
                if (Vector2.Distance(campo.transform.position, transform.position) < campo.size){
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


        if (dentro == 1){PlanetMovement();} else{
            //Esto se encarga del movimiento cuando el objeto se encuentra fuera de un planeta
            P.x = P.x + V.x * Time.deltaTime;
            P.y = P.y + V.y * Time.deltaTime;
            transform.position = P;
        }
        Destroy(gameObject, 60);
    }

    //Esta funcion se encarga del movimiento cuando el objeto se encuentra afectado por un planeta.
    void PlanetMovement()
    {
        A = (center.position - transform.position);
        F = g*((transform.localScale.x*center.localScale.x*60)/Mathf.Pow(A.magnitude, 2));
        V += (F * A.normalized * peso) * Time.deltaTime;
        Vector3 v3 = V;
        transform.position += v3 * Time.deltaTime;
    }

    public void OnCollision(GameObject coll)
    {
        Destroy(this.gameObject);
    }
}
