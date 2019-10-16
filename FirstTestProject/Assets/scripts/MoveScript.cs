using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{

    public Vector2 speed = new Vector2(10, 10);
    public Vector2 direction = new Vector2(-1, 0);
    private Vector2 zeroMovement = new Vector2(0, 0);
    public static bool isTimeStopped = false;

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    private int timeCount;
    private bool canMove;

    void Start()
    {
        timeCount = System.DateTime.Now.Second;
    }

    // Update is called once per frame
    void Update()
    {

        if (isTimeStopped && !canMove)
        {
            int currentTime = System.DateTime.Now.Second;
            if ((currentTime - timeCount) >= 2)
                isTimeStopped = false;
            else
                return;
        }

        timeCount = System.DateTime.Now.Second;
        if (timeCount > 57)
            timeCount -= 60;
        //else
        //  speed = originalSpeed;

        movement = new Vector2(
                speed.x * direction.x,
                speed.y * direction.y);
    }

    void FixedUpdate()
    {


        //timeCount = System.DateTime.Now.Second;
        //if (timeCount > 57)
        //    timeCount -= 60;
        ShotScript shot = gameObject.GetComponent<ShotScript>();
        HomingShotScript homingShot = gameObject.GetComponent<HomingShotScript>();
        canMove = false;
        if (shot != null)
            canMove = !shot.isEnemyShot;
        if (homingShot != null)
            canMove = true;

        if (isTimeStopped && !canMove)
        {
            //Debug.Log("Time Stopped");
            if (rigidbodyComponent == null)
                rigidbodyComponent = GetComponent<Rigidbody2D>();
            rigidbodyComponent.velocity = zeroMovement;
        }
        else
        {
            //Debug.Log("Time Not Stopped");
            if (rigidbodyComponent == null)
                rigidbodyComponent = GetComponent<Rigidbody2D>();
            rigidbodyComponent.velocity = movement;
        }
          //  Debug.Log("movement of " + gameObject.name + " = " + movement);
        //if (rigidbodyComponent == null)
        //    rigidbodyComponent = GetComponent<Rigidbody2D>();
        //rigidbodyComponent.velocity = movement;
    }
}
