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

    private Animator animator;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponentInChildren<Animator>();
        flechaDireccion.transform.localScale = new Vector3(Vo / 10, 2, 2);

        colliderController = FindObjectOfType<ColliderController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(Vo != 0){
                animator.SetTrigger("Shoot");
                ProjectileMovement1 pro = Instantiate(projectile, transform.position, transform.rotation) as ProjectileMovement1;
                colliderController.Circles.Add(pro.GetComponent<CircleColliderSim>());
                pro.Vo = Vo;
                pro.Angulo = (((Angulo + transform.eulerAngles.z) * Mathf.PI) / 180);

                gameManager.DisableMovement();
                
            }

        }
        if (Input.GetKey(KeyCode.E))
        {
            Vo = Mathf.Clamp(Vo + SpeedOfChangeVelocity * Time.deltaTime, 0, 25);
            flechaDireccion.transform.localScale = new Vector3(Vo/10,2, 2);
        } else if (Input.GetKey(KeyCode.Q))
        {
            Vo = Mathf.Clamp(Vo - SpeedOfChangeVelocity * Time.deltaTime, 10, 25);
            flechaDireccion.transform.localScale = new Vector3(Vo/10, 2, 2);
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            Angulo = Angulo + Input.GetAxisRaw("Vertical") * SpeedOfChangeAngle * Time.deltaTime;
            flechaDireccion.transform.RotateAround(transform.position, flechaDireccion.transform.forward, Input.GetAxisRaw("Vertical") * SpeedOfChangeAngle * Time.deltaTime);
        }
    }
}
