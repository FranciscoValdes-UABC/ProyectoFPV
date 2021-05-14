using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public  bool JuegoPausado = false;
    public GameObject MenuPausaUI;
    public GameObject MenuPausaOpcionesUI;
    private MenuPrincipal menuPrincipal;

    void Start()
    {
        menuPrincipal = FindObjectOfType<MenuPrincipal>();
    }

    // Update is called once per frame
    void Update()
    {
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
    public void Reanudar() {
        menuPrincipal.Ocultar(MenuPausaUI);
        menuPrincipal.Ocultar(MenuPausaOpcionesUI);
        Time.timeScale = 1f;
        JuegoPausado = false;
    }
    
}
