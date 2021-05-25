using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Disparo : MonoBehaviour
{
    //Guarda el prefab del proyectil a disparar.
    public ProjectileMovement1 projectile;
    
    //Hace referencia a el objeto que utilizamos como indicador de la direccion y fuerza del disparo.
    public GameObject flechaDireccion;

    //Esto guarda la velocidad inicial y el angulo del disparo.
    public float Vo;
    public float Angulo;

    //Este booleano se utiliza para evitar que el dejar el boton de disparo presionado dispare infinitas balas.
    public bool m_isAxisInUse = false;

    //Esta es la velocidad con la que el jugador puede cambiar la velocidad y el angulo de disparo.
    public float SpeedOfChangeVelocity;
    public float SpeedOfChangeAngle;

    //Referencia a el animador del jugador.
    private Animator animator;

    //Referencias a texto en pantalla.
    public TextMeshProUGUI posicion_txt;
    public TextMeshProUGUI velocidad_txt;
    public TextMeshProUGUI aceleracion_txt;

    //Se hace referencia al Controlador de colliders y al GameManager.
    private ColliderController colliderController;
    private GameManager gameManager;


    // Esta funcion se llama al comienzo del juego.
    void Start()
    {
        //Se referencia el GameManager en escena, el controllador de animaciones del jugador y el ColliderController.
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponentInChildren<Animator>();
        colliderController = FindObjectOfType<ColliderController>();

        //Se le dan valores iniciales al tamaño de la flecha.
        flechaDireccion.transform.localScale = new Vector3(Vo / 10, 2, 2);
    }

    //Esta funcion se llama una vez en cada fotograma.
    void Update()
    {

        //Si se presiona la tecla relacionada con disparar.
        if (Input.GetAxis("Fire1") != 0)
        {
            //Si esta tecla no estaba en uso anteriormente.
            if (m_isAxisInUse == false)
            {
                //Si la velocidad inicial no es 0.
                if (Vo != 0)
                {
                    //Entonces se inicia la animacion de "Shoot" del jugador.
                    //Se instancia un proyectil, se le manda su collider a el GameManager.
                    //Al proyectil se le asigna su velocidad inicial, Angulo y los valores para sus campos de texto.
                    animator.SetTrigger("Shoot");
                    ProjectileMovement1 pro = Instantiate(projectile, transform.position, transform.rotation) as ProjectileMovement1;
                    colliderController.Circles.Add(pro.GetComponent<CircleColliderSim>());
                    pro.Vo = Vo;
                    pro.Angulo = (((Angulo + transform.eulerAngles.z) * Mathf.PI) / 180);
                    pro.posicion_txt = posicion_txt;
                    pro.velocidad_txt = velocidad_txt;
                    pro.aceleracion_txt = aceleracion_txt;

                    //Se desactiva el movimiento de ambos jugadores.
                    gameManager.DisableMovement();

                }
                //Se marca que la tecla esta en uso.
                m_isAxisInUse = true;
            }
        }
        //Se marca que la tecla ya no esta en uso cuando el valor de ese axis es 0
        if (Input.GetAxisRaw("Fire1") == 0)
        {
            m_isAxisInUse = false;
        }

        //Si el jugador presiona E entonces se aumentara la velocidad inicial, si presiona Q ocurrira lo inverso.
        //Si el jugador presiona las teclas relacionadas con el movimiento vertical, dichos cambios se reflejaran en el angulo del disparo.
        //Los cambios se veran reflejados en la escala y rotacion de la flecha.
        if (Input.GetKey(KeyCode.E))
        {
            Vo = Mathf.Clamp(Vo + SpeedOfChangeVelocity * Time.deltaTime, 10, 30);
            flechaDireccion.transform.localScale = new Vector3(Vo/10,2, 2);
        } else if (Input.GetKey(KeyCode.Q))
        {
            Vo = Mathf.Clamp(Vo - SpeedOfChangeVelocity * Time.deltaTime, 10, 30);
            flechaDireccion.transform.localScale = new Vector3(Vo/10, 2, 2);
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            Angulo = Angulo + Input.GetAxisRaw("Vertical") * SpeedOfChangeAngle * Time.deltaTime;
            flechaDireccion.transform.RotateAround(transform.position, flechaDireccion.transform.forward, Input.GetAxisRaw("Vertical") * SpeedOfChangeAngle * Time.deltaTime);
        }
    }
}
