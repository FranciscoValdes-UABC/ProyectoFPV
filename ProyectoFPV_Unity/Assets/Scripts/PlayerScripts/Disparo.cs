using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public ProjectileMovement1 projectile;
    public GameObject flechaDireccion;
    public float Vo;
    public float Angulo;
    private ColliderController colliderController;

    public float SpeedOfChangeVelocity;
    public float SpeedOfChangeAngle;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
                if (gameObject.tag == "Player1")
                {
                    gameManager.ChangeTurn(1);
                }
                else {
                    gameManager.ChangeTurn(0);
                }
            }

        }
        if (Input.GetKey(KeyCode.E))
        {
            Vo = Vo + SpeedOfChangeVelocity * Time.deltaTime;
            flechaDireccion.transform.localScale = new Vector3(Vo/50, 0.2f, 0.2f);
        } else if (Input.GetKey(KeyCode.Q))
        {
            Vo = Vo - SpeedOfChangeVelocity * Time.deltaTime;
            flechaDireccion.transform.localScale = new Vector3(Vo/50, 0.2f, 0.2f);
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            Angulo = Angulo + Input.GetAxisRaw("Vertical") * SpeedOfChangeAngle * Time.deltaTime;
            flechaDireccion.transform.RotateAround(transform.position, flechaDireccion.transform.forward, Input.GetAxisRaw("Vertical") * SpeedOfChangeAngle * Time.deltaTime);
        }
    }
}
