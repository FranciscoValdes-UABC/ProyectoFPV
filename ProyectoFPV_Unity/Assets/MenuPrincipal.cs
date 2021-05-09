using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public AudioSource Ambiente;
    public static bool JuegoPausado = false;
    public GameObject MenuPausaUI;

    void Start()
    {
        Ambiente.Play();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            if (JuegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausa();
            }
        }
    }

    public void JugarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MenuInicial()
    {
        SceneManager.LoadScene(0);
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

    public void Reanudar()
    {
        MenuPausaUI.SetActive(false);
        Time.timeScale = 1f;
        JuegoPausado = false;
    }

    void Pausa()
    {
        MenuPausaUI.SetActive(true);
        Time.timeScale = 0f;
        JuegoPausado = true;
    }
}
