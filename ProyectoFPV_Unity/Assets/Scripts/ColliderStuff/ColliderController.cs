using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public List<BoxColliderSim> Boxes;
    public List<CircleColliderSim> Circles;

    //Se ignoraran las colisiones que ocurran entre los objetos que tengan las tags contenidas en estas listas
    //El sistema funciona como pares, los tags que tengan el mismo indice en las listas se excluiran entre ellos
    //Para esto las listas siempre tendran que ser del mismo tamaño
    public List<string> TagsToExcludeP1;
    public List<string> TagsToExcludeP2;

    void Start()
    {
        Boxes = FindObjectsOfType<BoxColliderSim>().ToList();
        Circles = FindObjectsOfType<CircleColliderSim>().ToList();
    }

    void Update()
    {
        Circles.RemoveAll(item => item == null);
        Boxes.RemoveAll(item => item == null);
        //Esto revisa si existen colisiones entre circulos y rectangulos
        foreach ( CircleColliderSim j in Circles)
        {

            foreach (BoxColliderSim i in Boxes){

                //Esto sirve para excluir las colisiones de los objetos que contengan los tags definidos anteriormente
                bool excluir = false;
                for (int h = 0; h < TagsToExcludeP1.Count; h++){
                    if(i.gameObject.tag == TagsToExcludeP1[h] && j.gameObject.tag == TagsToExcludeP2[h]){
                        excluir = true;                
                    } else if (i.gameObject.tag == TagsToExcludeP2[h] && j.gameObject.tag == TagsToExcludeP1[h])
                    {
                        excluir = true;
                    }
                }
 
                if (excluir) { continue; }

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
                    //Esta funcion busca, en todos los scripts que hereden de MonoBehaviour dentro de el objeto, una funcion con el
                    //nombre "OnCollision" le manda como argumento el objeto con el que colisiono y el ultimo argumento indica que no
                    //importa si no encuentra ninguna funcion con ese nombre
                    j.gameObject.SendMessage("OnCollision", i.gameObject, SendMessageOptions.DontRequireReceiver);
                    i.gameObject.SendMessage("OnCollision", j.gameObject, SendMessageOptions.DontRequireReceiver);
                }
            }

            //Detecta colision entre circulos
            foreach (CircleColliderSim i in Circles)
            {
                float Dx = j.transform.position.x - i.transform.position.x;
                float Dy = j.transform.position.y - i.transform.position.y;
                float D = Mathf.Sqrt(Dx * Dx + Dy * Dy);

                if (i == j) {
                    continue;
                }

                //Si la distancia entre los centros de los circulos es mayor al radio de cualquiera de los dos entonces existe una colision
                if (D <= j.size + i.size)
                {
                    print("Circle Colision!");
                    //Esta funcion busca, en todos los scripts que hereden de MonoBehaviour dentro de el objeto, una funcion con el
                    //nombre "OnCollision" le manda como argumento el objeto con el que colisiono y el ultimo argumento indica que no
                    //importa si no encuentra ninguna funcion con ese nombre
                    j.gameObject.SendMessage("OnCollision", i.gameObject, SendMessageOptions.DontRequireReceiver);
                    i.gameObject.SendMessage("OnCollision", j.gameObject, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
