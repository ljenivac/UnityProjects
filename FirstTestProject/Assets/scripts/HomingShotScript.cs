using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingShotScript : MonoBehaviour
{
    // 1 - Designer variables

    /// <summary>
    /// Damage inflicted
    /// </summary>
    public int damage = 1;
    public float rotateSpeed = 200.0f;
    public float speed = 5.0f;
    private Transform target;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 2 - Limited time to live to avoid any leak
        Destroy(gameObject, 20); // 20sec
    }

    void FixedUpdate()
    {
        MoveScript move = gameObject.GetComponent<MoveScript>();
        GameObject gos = FindClosestEnemy();
        if(gos != null)
        {
            target = gos.transform;

            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * rotateSpeed;
            move.direction = (Vector2)transform.up;
        }
        //move.direction.x = transform.up.x;
        //move.direction.y = transform.up.y;
        // rb.velocity = transform.up * speed;
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
