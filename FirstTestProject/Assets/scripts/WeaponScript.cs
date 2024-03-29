﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    //--------------------------------
    // 1 - Designer variables
    //--------------------------------

    /// <summary>
    /// Projectile prefab for shooting
    /// </summary>
    public Transform shotPrefab;
    public Transform homingShotPrefab;
    private Transform homingShotTransform;

    /// <summary>
    /// Cooldown in seconds between two shots
    /// </summary>
    public float shootingRate = 0.25f;

    //--------------------------------
    // 2 - Cooldown
    //--------------------------------

    private float shootCooldown;

    void Start()
    {
        shootCooldown = 0f;
    }

    void Update()
    {
        if (shootCooldown > 0f)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    //--------------------------------
    // 3 - Shooting from another script
    //--------------------------------

    /// <summary>
    /// Create a new projectile if possible
    /// </summary>
    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Assign position
            shotTransform.position = transform.position;

            // The is enemy property
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // Make the weapon shot always towards it
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null)
            {
                move.direction = this.transform.right; // towards in 2D space is the right of the sprite
            }
        }
    }

    public void AttackHomingShot()
    {
        if (CanHomingAttack)
        {
            shootCooldown = shootingRate + 0.5f;

            // Create a new shot
            homingShotTransform = Instantiate(homingShotPrefab) as Transform;

            // Assign position
            homingShotTransform.position = transform.position;

            MoveScript move = homingShotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null)
            {
                //Debug.Log("Homing missile");
                move.direction = this.transform.up; // towards in 2D space is the up of the sprite
                //move.direction = new Vector2(0, 1); // towards in 2D space is the up of the sprite
            }
        }
    }

    /// <summary>
    /// Is the weapon ready to create a new projectile?
    /// </summary>
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }

    public bool CanHomingAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}
