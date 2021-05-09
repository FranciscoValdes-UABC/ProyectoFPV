using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Lista de todos los jugadores
    //Jugador 1 = 0
    //Jugador 2 = 1
    public GameObject[] Jugadores;

    void Start()
    {
        ChangeTurn(0);
    }

    
    public void ChangeTurn(int jA)
    {
        for (int i = 0; i < Jugadores.Length; i++) {
            if (i == jA)
            {
                Jugadores[i].GetComponent<Disparo>().enabled = true;
                Jugadores[i].GetComponent<PolarMovement>().enabled = true;
                continue;
            }
            else {
                Jugadores[i].GetComponent<Disparo>().enabled = false;
                Jugadores[i].GetComponent<PolarMovement>().enabled = false;
            }
        }
    }

    public void Win() {
        print("Ya gano un master");
    }
}
