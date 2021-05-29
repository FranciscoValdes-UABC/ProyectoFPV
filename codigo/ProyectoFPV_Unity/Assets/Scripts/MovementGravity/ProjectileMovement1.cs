using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectileMovement1 : MonoBehaviour
{

    public float speed;
    public CampoGravitatorio[] camposGravitatorios;
    public TextMeshProUGUI posicion_txt;
    public TextMeshProUGUI velocidad_txt;
    public TextMeshProUGUI aceleracion_txt;

    //Tiempo maximo
    public float maxTime;

    public float Vo;
    //Esto se referencia en el script disparo
    public float Angulo;
    public float g;
    public float peso;
    //Posicion, velocidad y angulo
    Vector2 P;
    Vector2 V;
    Vector2 A;
    float F;
    float time;

    //Esta variable será el centro cuando se confirme que está en un planeta
    //Dentro define si el proyectil esta dentro de un planeta, si es 0 significa que no
    private Transform center;
    private int dentro = 0;


    //Aqui se guardan prefabs de las explosiones de impacto
        //0 - Impacta con planeta
        //1 - Impacta con jugador 
    public GameObject[] ImpactExplosion;

    //Referencia al gamemanager que se encuentra en juego
    private GameManager gameManager;


    //Esta funcion se llama en el comienzo del juego.
    void Start()
    {
        //Se encuentra el objeto que contiene a GameManager
        gameManager = FindObjectOfType<GameManager>();

        //Se definen los valores iniciales de la posicion y velocidad
        P = new Vector2(transform.position.x, transform.position.y);
        V = new Vector2(Vo * Mathf.Cos(Angulo), Vo * Mathf.Sin(Angulo));
        peso = 1;

        //Lista en donde se encuentran todos los planetas
        camposGravitatorios = FindObjectsOfType<CampoGravitatorio>();

        //Se muestran los valores en el HUD dentro del juego
        posicion_txt.text = "Posicion = " + P;
        velocidad_txt.text = "Velocidad = " + V;
        aceleracion_txt.text = "Aceleracion = " + A;
    }

    //Esta funcion se llama una vez en cada fotograma.
    void Update()
    {
        //Cronometro para que la bala se destruya y cambie el turno.
            //Una vez cambia el turno se genera una explosion y se destruye el proyectil
        time += Time.deltaTime;
        if (time > maxTime) {
            CambiarTurno();
            GameObject exp = Instantiate(ImpactExplosion[2], transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);
        }


        //Si el proyectil no se encuentra dentro de un campo de gravedad entonces itera sobre los campos de gravedades dentro del juego para encontrar el momento en el que se entre a uno
            //Si se entra a un campo de gravedad entonces:
                //Se cambia la variable Dentro para reflejar el cambio.
                //La variable g se iguala a la gravedad del campo gravitatorio
                //Se encuentra el angulo instantaneo en el que se dirige el proyectil al momento de entrar en el campo de gravedad
                //La variable center se iguala al centro del campo de gravedad al que se entro.
        //Si el proyectil se encuentra en un campo de gravedad entonces se verifica que este no se aleje lo suficiente para salir del mismo.
            //Si se sale de un campo de gravedad entonces:
                //Se reinician las variables P, dentro, g y A.
        if (dentro == 0){
            foreach (CampoGravitatorio campo in camposGravitatorios) {
                if (Vector2.Distance(campo.transform.position, transform.position) < campo.size){
                    dentro = 1;
                    g = campo.gravedad;
                    A = (campo.transform.position - transform.position).normalized;
                    center = campo.transform;
                }
            }
        }
        else if (Vector2.Distance(center.transform.position, transform.position) > center.GetComponent<CampoGravitatorio>().size)
        {
            P = transform.position;
            dentro = 0;
            g = 0;
            A = new Vector2(0, 0);
        }

        //Si el proyectil se encuentra en un campo de gravedad entonces: 
            //El movimiento usado es el definido en la funcion PlanetMovement()
        //Si no se encuentra dentro de un campo de gravedad entonces:
            //La nueva posicion de X y Y se basa unicamente en el vector de velocidad, 
            //el vector de velocidad no se modifica debido a que cuando un proyectil no esta 
            //en el espacio esto significa que esta en el espacio. 
        if (dentro == 1){PlanetMovement();} else{
            P.x = P.x + V.x * Time.deltaTime;
            P.y = P.y + V.y * Time.deltaTime;
            transform.position = P;
        }

        //Estas lineas funcionan para modificar los valores mostrados dentro del juego.
        posicion_txt.text = "Posicion = (" + transform.position.x.ToString("f1") + ", " + transform.position.y.ToString("f1") + ")";
        velocidad_txt.text = "Velocidad = " + V;
        aceleracion_txt.text = "Aceleracion = " + A;
    }

    //Esta funcion se llama cuando se tiene que cambiar un turno.
    void CambiarTurno() {
        //Si es el jugador 1 entonces se indica que se tiene que dal el turno al jugador 2, ocurre lo inverso en otro caso
        if (gameObject.tag == "ProjectPla1"){
            gameManager.ChangeTurn(1);
        }else{
            gameManager.ChangeTurn(0);
        }
    }

    //Esta funcion se encarga del movimiento cuando el objeto se encuentra afectado por un planeta.
    void PlanetMovement(){
        //El angulo al que se calcula restando la posicion actual con la posicion del centro.
        A = (center.position - transform.position);
        /*
        La fuerza se obtiene a base de la ley de gravitacion universal
        la masa de los objetos la basamos en la escala que se les dio dentro del juego.
        La distancia entre los objetos se puede obtener facilmente al obtener la magnitud del angulo.
        */ 
        F = g*((transform.localScale.x*center.localScale.x)/Mathf.Pow(A.magnitude, 2));
        /*
         Se obtiene la aceleracion dividiendo la fuerza entre la masa del proyectil, para darle
         direccion al movimiento se multiplica por el vector de aceleracion normalizado.
         El vector obtenido de velocidad se le suma despues al de posicion.
         */

        V += ((F * A.normalized) / transform.localScale.x) * Time.deltaTime;
        Vector3 v3 = V;
        transform.position += v3 * Time.deltaTime;
    }

    //Esta funcion se ejecuta cuando se detecta una colision
    public void OnCollision(GameObject coll)
    {
        //Primera mente se cambia de turno y se destruye el objeto de proyectil
        CambiarTurno();
        Destroy(this.gameObject);

        //Esto se utiliza para generar la explosion, como existen diferentes explosiones para 
        //cuando el proyectil choca contra un objeto y cuando choca con un jugador, por lo que esto
        //tiene que ser verificado
        GameObject Explosion;
        Vector3 posImp = transform.position; ;
        if (coll.tag == "Player1" || coll.tag == "Player2")
        {
            posImp = coll.transform.position;
            Explosion = ImpactExplosion[1];
        }
        else {
            Explosion = ImpactExplosion[0];
        }
        GameObject exp = Instantiate(Explosion, posImp, transform.rotation) as GameObject;
        //Esto define la rotacion del explosivo para que este este de acuerdo al planeta en el que se choca.
        exp.transform.up = transform.position - center.position;
    }
}
