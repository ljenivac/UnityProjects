﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(50, 50);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    // Update is called once per frame
    void Update()
    {
        bool shoot = Input.GetButtonDown("Fire1");
        bool laserShoot = Input.GetButtonDown("Fire2");

        if(shoot)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null)
                weapon.Attack(false);
            SoundEffectsHelper.Instance.MakePlayerShotSound();
        }

        if (laserShoot)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null)
                weapon.AttackLaser();
            SoundEffectsHelper.Instance.MakePlayerShotSound();
        }
        else
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null)
                weapon.DestroyLaser();
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movement = new Vector2(
            speed.x * inputX,
            speed.y * inputY);
    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        rigidbodyComponent.velocity = movement;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool damagePlayer = false;

        // Collision with enemy
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            // Kill the enemy
            HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
            if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);

            damagePlayer = true;
        }

        // Damage the player
        if (damagePlayer)
        {
            HealthScript playerHealth = this.GetComponent<HealthScript>();
            if (playerHealth != null) playerHealth.Damage(1);
        }

        /*TimeStopScript item = collision.gameObject.GetComponent<TimeStopScript>();
        if(item != null)
        {
            System.Console.WriteLine("hit an item");
            Destroy(item.gameObject);
            return;
        }*/
    }
}
