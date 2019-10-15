using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{

    public Vector2 speed = new Vector2(10, 10);
    public Vector2 direction = new Vector2(-1, 0);
    public Vector2 zeroMovement = new Vector2(0, 0);
    public static bool isTimeStopped = false;

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    private int timeCount;
    private Vector2 originalSpeed;

    void Start()
    {
        timeCount = System.DateTime.Now.Second;
        originalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeStopped)
        {
            Debug.Log("Move Script time stopped!");
            int currentTime = System.DateTime.Now.Second;
            if ((currentTime - timeCount) >= 3)
                isTimeStopped = false;

            speed = new Vector2(0, 0);
            //Debug.Log("movement = " + movement);
            Debug.Log("timeCount = " + timeCount);
            Debug.Log("CurrentTime = " + currentTime);
            return;
        }
        else
            speed = originalSpeed;

        movement = new Vector2(
                speed.x * direction.x,
                speed.y * direction.y);
    }

    void FixedUpdate()
    {


        //timeCount = System.DateTime.Now.Second;
        //if (timeCount > 57)
        //    timeCount -= 60;
        if (isTimeStopped)
        {
            if (rigidbodyComponent == null)
                rigidbodyComponent = GetComponent<Rigidbody2D>();
            rigidbodyComponent.velocity = zeroMovement;
        }
        else
        {
            if (rigidbodyComponent == null)
                rigidbodyComponent = GetComponent<Rigidbody2D>();
            rigidbodyComponent.velocity = movement;
        }
            Debug.Log("movement of " + gameObject.name + " = " + movement);
        //if (rigidbodyComponent == null)
        //    rigidbodyComponent = GetComponent<Rigidbody2D>();
        //rigidbodyComponent.velocity = movement;
    }
}
