using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public ProjectileMovement1 projectile;
    public float Vo;
    public float Angulo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(Vo != 0){
                ProjectileMovement1 pro = Instantiate(projectile, transform.position, transform.rotation) as ProjectileMovement1;
                pro.Vo = Vo;
                pro.Angulo = (Angulo * Mathf.PI) / 180;
            }
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Vo = Vo + Input.GetAxisRaw("Horizontal")/10;
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            Angulo = Angulo + Input.GetAxisRaw("Vertical") / 10;
        }
    }
}
