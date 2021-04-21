using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarMovement : MonoBehaviour
{
    public Transform center;
    public float speed;
    private Vector2 pos;



    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            horizontalPolarMovement((int)Mathf.Sign(Input.GetAxis("Horizontal")));
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            verticalPolarMovement((int)Mathf.Sign(Input.GetAxis("Vertical")));
        }
    }

    void horizontalPolarMovement(int sign)
    {

        Vector3 targetDirection = center.position - transform.position;
        targetDirection.z = 0;
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * targetDirection;
        transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);



        //pos.x += Time.deltaTime;
        pos.y += Time.deltaTime * (speed * sign);
        transform.position = (Vector2)center.position + new Vector2(pos.x * Mathf.Sin(pos.y), pos.x * Mathf.Cos(pos.y));
    }

    void verticalPolarMovement(int sign)
    {

        pos.x += Time.deltaTime * (speed * sign);
        transform.position = (Vector2)center.position + new Vector2(pos.x * Mathf.Sin(pos.y), pos.x * Mathf.Cos(pos.y));
    }
}
