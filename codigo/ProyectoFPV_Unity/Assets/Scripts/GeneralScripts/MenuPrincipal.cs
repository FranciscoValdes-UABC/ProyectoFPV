using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuPrincipal : MonoBehaviour
{
    //Estas variables se utilizan para modificar la musica dentro del juego
    public AudioSource Ambiente;
    public AudioMixer audioMixer;
    float volumen;



    //Esta funcion se ejecuta al iniciar la escena
    void Start(){
        //Inicia la musica de fondo.
        Ambiente.Play();
    }

    //Las siguientes funciones ocurren en el momento en el que un boton se presiona.

    //Esta funcion es la encargada de hacer visible un menu.
    public void Mostrar(GameObject MenuPausaUI)
    {
        MenuPausaUI.SetActive(true);

    }

    //Esta funcion es la encargada de ocultar un menu.
    public void Ocultar(GameObject MenuPausaUI)
    {
        MenuPausaUI.SetActive(false);
    }


    //Esta funcion esta definida para ejecutarse en un menu de pausa, reanuda el juego.
    public void Reanudar(MenuPausa menuPausa)
    {
        menuPausa.Reanudar();

    }

    //Esta funcion cierra el juego.
    public void SalirJuego()
    {
        print("Quit");
        Application.Quit();
    }

    //Esta funcion carga una escena que tenga la clave i.
    public void CargarEscena(int i)
    {
        SceneManager.LoadScene(i);
    }

    //Esta funcion modifica el volumen de la musica.
    public void Volumen(float volumen)
    {
        audioMixer.SetFloat("volumenMusica", volumen);
    }

    //Esta funcion modifica el volumen de los efectos en el juego.
    public void VolumenEfectos(float volumenEf)
    {
        audioMixer.SetFloat("volumenEfectos", volumenEf);
    }


}
