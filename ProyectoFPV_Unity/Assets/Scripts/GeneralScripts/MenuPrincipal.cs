using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public AudioSource Ambiente;


    void Start()
    {
        Ambiente.Play();
    }

    public void Mostrar(GameObject MenuPausaUI)
    {
        MenuPausaUI.SetActive(true);

    }

    public void Ocultar(GameObject MenuPausaUI)
    {
        MenuPausaUI.SetActive(false);
    }

    public void Reanudar(MenuPausa menuPausa)
    {
        menuPausa.Reanudar();

    }

    public void SalirJuego()
    {
        print("Quit");
        Application.Quit();
    }

    public void CargarEscena(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void Volumen(float volumen)
    {
        Ambiente.volume = volumen;
    }

   
}
