using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    //Valor booleano que representa cuando el juego esta pausado.
    public  bool JuegoPausado = false;

    //Se guardan los objetos en el juego que representan el menu de pausa y el submenu de opciones.
    public GameObject MenuPausaUI;
    public GameObject MenuPausaOpcionesUI;

    //Se hace referencia al script de MenuPrincipal para poder utilizar sus funciones.
    private MenuPrincipal menuPrincipal;


    //Esta funcion se ejecuta al iniciar la escena
    void Start()
    {
        //Se encuentra la instancia de menu principal que se encuentre en escena.
        menuPrincipal = FindObjectOfType<MenuPrincipal>();
    }

    //Esta funcion se ejecuta una vez cada fotograma.
    void Update()
    {
        //Si el jugador presiona la tecla de escape entonces:
            //Se verifica si el juego esta pausado, de ser asi:
                //Se manda a llamar la funcion de Reanudar.
            //Si el juego no esta pausado entonces:
                //Se muestra el menu de pausa.
                //Se actualiza el valor de la variable JuegoPausado.
                //La escala de tiempo en el juego se cambia a 0, esto es equivalente a 
                //"pausar el tiempo" dentro del juego.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (JuegoPausado)
            {
                Reanudar(); 
            }
            else
            {
                menuPrincipal.Mostrar(MenuPausaUI);
                JuegoPausado = true;
                Time.timeScale = 0f;
            }
        }
    }

    //Esta funcion se ejecuta cuando se desea reanudar el juego.
    public void Reanudar() {
        //Se ocultan los menus relacionados a pausa.
        //Se cambia la escala de tiempo de vuelta a 1.
        //Se actualiza el valor de la variable JuegoPausado.
        menuPrincipal.Ocultar(MenuPausaUI);
        menuPrincipal.Ocultar(MenuPausaOpcionesUI);
        Time.timeScale = 1f;
        JuegoPausado = false;
    }
    
}
