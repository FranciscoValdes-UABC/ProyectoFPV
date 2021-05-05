using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public ProjectileMovement1 projectile;
    public float Vo;
    public float Angulo;
    private ColliderController colliderController;

    public float SpeedOfChangeVelocity;
    public float SpeedOfChangeAngle;

    // Start is called before the first frame update
    void Start()
    {
        colliderController = FindObjectOfType<ColliderController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(Vo != 0){
                ProjectileMovement1 pro = Instantiate(projectile, transform.position, transform.rotation) as ProjectileMovement1;
                colliderController.Circles.Add(pro.GetComponent<CircleColliderSim>());
                pro.Vo = Vo;
                pro.Angulo = (((Angulo + transform.eulerAngles.z) * Mathf.PI) / 180);
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            Vo = Vo + SpeedOfChangeVelocity * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.Q))
        {
            Vo = Vo - SpeedOfChangeVelocity * Time.deltaTime;
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            Angulo = Angulo + Input.GetAxisRaw("Vertical") * SpeedOfChangeAngle * Time.deltaTime ;
        }
    }
}
