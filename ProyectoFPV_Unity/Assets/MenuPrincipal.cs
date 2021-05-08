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

    public void JugarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SalirJuego()
    {
        print("Quit");
        Application.Quit();
    }

    public void Volumen(float volumen)
    {
        Ambiente.volume = volumen;
    }
}
