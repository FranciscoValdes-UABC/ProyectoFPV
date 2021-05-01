using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public List<GameObject> objects;
    public List<BoxColliderSim> Boxes;
    public List<CircleColliderSim> Circles;

    public Vector2 pos1;
    public Vector2 pos2;

    void Start()
    {
       
        foreach (GameObject i in objects)
        {
            BoxColliderSim box = i.GetComponent<BoxColliderSim>();
            CircleColliderSim circle = i.GetComponent<CircleColliderSim>();
            if (box != null)
            {
                Boxes.Add(box);
            }
            if (circle != null)
            {
                Circles.Add(circle);
            }
        }
    }

    void Update()
    {
        //Esto revisa si existen colisiones entre circulos y rectangulos
        foreach (BoxColliderSim i in Boxes)
        {
            foreach (CircleColliderSim j in Circles)
            {

            

                pos1 = i.transform.TransformPoint(new Vector3( - (i.sizes[0] / 2),  - (i.sizes[1] / 2),0));
                pos2 = i.transform.TransformPoint(new Vector3( + (i.sizes[0] / 2),  + (i.sizes[1] / 2),0));

                //Como el cuadrado puede rotar tal vez sea mejor tomar las coordenadas del cuadrado como las del origen

                Vector2 C = i.transform.InverseTransformPoint(new Vector3(j.transform.position.x, j.transform.position.y));

                //Encuentra los valores de el punto mas cercano del cuadrado al circulo.
                float Xn = Mathf.Max(-(i.sizes[0] / 2), Mathf.Min(C.x, +(i.sizes[0] / 2)));
                float Yn = Mathf.Max(-(i.sizes[1] / 2), Mathf.Min(C.y, +(i.sizes[1] / 2)));

                //De esta forma obtenemos el punto dentro de las coordenadas del circulo para poder calcular su distancia al mismo de forma correcta
                float Dx = C.x - Xn;
                float Dy = C.y - Yn;

                //Si dicha distancia es menor a el radio del circulo entones existe una interseccion
                if ((Dx * Dx + Dy * Dy) <= j.size * j.size)
                {
                    print("Colision!");
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(pos1, .1f);
        Gizmos.DrawSphere(pos2, .1f);
    }
}
