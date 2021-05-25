using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnim : StateMachineBehaviour
{


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    /* Esta es una funcion especifica de unity y el sistema de animacion integrado, como se mencion arriba
       cuando se acabe la transicion de animacion el objeto se eliminara, se manda a eliminar el padre del objeto
       debido a que el objeto animado realmente es un hijo de un objeto transparente, esto se hizo para facilitar 
       su posicionamiento.
    */
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.transform.parent.gameObject);
    }


}
