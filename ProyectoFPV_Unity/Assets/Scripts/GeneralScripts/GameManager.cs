using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Lista de todos los jugadores
    //Jugador 1 = 0
    //Jugador 2 = 1
    public GameObject[] Jugadores;
    public GameObject[] Fondos;

    void Start()
    {
        ChangeTurn(0);
    }

    public void DisableMovement()
    {
        Jugadores[0].GetComponent<Disparo>().enabled = false;
        Jugadores[0].GetComponent<PolarMovement>().enabled = false;
        Jugadores[1].GetComponent<Disparo>().enabled = false;
        Jugadores[1].GetComponent<PolarMovement>().enabled = false;
    }

    public void ChangeTurn(int jA)
    {
        StartCoroutine("changeBackground", jA);
 

        Jugadores[jA].GetComponent<Disparo>().enabled = true;
        Jugadores[jA].GetComponent<PolarMovement>().enabled = true;

        Jugadores[Mathf.Abs(jA - 1)].GetComponent<Disparo>().enabled = false;
        Jugadores[Mathf.Abs(jA - 1)].GetComponent<PolarMovement>().enabled = false;
            
    }
    

    IEnumerator changeBackground(int a) {
        
        for (float k = 0; k <= 1; k += 0.01f){
            Fondos[a].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, k);
            yield return null;
        }
        for (float j = 1; j >= 0; j -= 0.01f){
            Fondos[Mathf.Abs(a-1)].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, j);
            yield return null;
        }
            
        
    }

    public void Win() {
        print("Ya gano un master");
    }
}
