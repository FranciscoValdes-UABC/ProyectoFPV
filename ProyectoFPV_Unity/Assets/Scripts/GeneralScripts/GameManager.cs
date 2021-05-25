using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Lista de todos los jugadores
    //Jugador 1 = 0
    //Jugador 2 = 1
    public GameObject[] Jugadores;

    //Aqui se guardaran la lista de los objetos que funcionaran como fondo en el juego.
        //Fondo del Jugador 1 = 0
        //Fondo del Jugador 2 = 1
    public GameObject[] Fondos;

    //Aqui se guarda el objeto que representara el menu que aparrece cuando se gana.
    public GameObject menuGanar;
    //Esto se utiliza para modificar el texto cuando se gana, como se utiliza otra
    //paqueteria para este texto se agrega de esta forma.
    public TMPro.TextMeshProUGUI textGanar;

    //Se hace referencia al script de MenuPrincipal para poder utilizar sus funciones.
    private MenuPrincipal menuPrincipal;



    //Esta funcion se ejecuta al iniciar la escena
    void Start(){
        //Se encuentra la instancia de menu principal que se encuentre en escena.
        menuPrincipal = FindObjectOfType<MenuPrincipal>();

        //Para comenzar el juego se utiliza la funcion de ChangeTurn dando control al primer jugador.
        ChangeTurn(0);
    }

    //Esta funcion desactiva el movimiento y la habilidad de disparar a todos los jugadores
    //Esto se utiliza para cuando exista una bala en aire.
    public void DisableMovement()
    {
        /*Para desactivar el movimiento y habilidad de disparo podemos acceder directamente a los scripts
         * de cada jugador y desactivarlos.
        */
        Jugadores[0].GetComponent<Disparo>().enabled = false;
        Jugadores[0].GetComponent<PolarMovement>().enabled = false;
        Jugadores[1].GetComponent<Disparo>().enabled = false;
        Jugadores[1].GetComponent<PolarMovement>().enabled = false;
    }

    //Esta funcion se ejecuta cuando se quiere cambiar de jugador, recibe como argumento el indice del jugador al que se le quiere
    //dar control.
    public void ChangeTurn(int jA)
    {
        //Se ejecuta la corutina definida para cambiar el fondo al del jugador correspondiente.
        StartCoroutine("changeBackground", jA);

        /*Las siguientes lineas de codigo activan el movimiento y habilidad de disparo a el jugador correspondiente
         * y lo desactivan al otro.
        */
        Jugadores[jA].GetComponent<Disparo>().enabled = true;
        Jugadores[jA].GetComponent<PolarMovement>().enabled = true;

        Jugadores[Mathf.Abs(jA - 1)].GetComponent<Disparo>().enabled = false;
        Jugadores[Mathf.Abs(jA - 1)].GetComponent<PolarMovement>().enabled = false;
            
    }
    
    /* Esto se ejecuta cuando existe un cambio de turno.
     * Debido a que el cambio de fondo se busca hacer de forma gradual y no en un solo fotograma
     * es por esto que se utiliza un IEnumerator, este puede ser ejecutado a lo largo de muchos fotogramas
     * siempre que se tenga un yield return dentro de las iteraciones.
    */
    IEnumerator changeBackground(int a) {
        
        /*Los siguientes fors aumentan el valor de alpha de la imagen indicada y disminullen el de la otra hasta que
         * el de la primera es 1 y la segunda es 0.
        */
        for (float k = 0; k <= 1; k += 0.01f){
            Fondos[a].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, k);
            yield return null;
        }
        for (float j = 1; j >= 0; j -= 0.01f){
            Fondos[Mathf.Abs(a-1)].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, j);
            yield return null;
        }
            
        
    }

    //Esta funcion se ejecuta en el momento en el que un jugador gana.
    public void Win(int a) {
        //Se cambia el texto para concordar con el ganador y se muestra el menu de fin de juego.
        textGanar.text = "Ganaste jugador " + (a);
        menuPrincipal.Mostrar(menuGanar);
    }
}
