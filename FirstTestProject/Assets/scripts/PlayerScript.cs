using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(50, 50);
    private Vector2 origSpeed = new Vector2(50, 50);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    public GameObject restartButton;

    void Start()
    {
        origSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        bool shoot = Input.GetButtonDown("Fire1");
        bool homingShoot = Input.GetButtonDown("Fire2");

        if(shoot)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null)
                weapon.Attack(false);
            SoundEffectsHelper.Instance.MakePlayerShotSound();
        }

        if (homingShoot)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null)
                weapon.AttackHomingShot();
            SoundEffectsHelper.Instance.MakePlayerShotSound();
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if (transform.position.x < Camera.main.transform.position.x && System.Math.Abs(transform.position.x - Camera.main.transform.position.x) > 17)
            if (inputX < 0)
                inputX = 0;

        if (transform.position.x > Camera.main.transform.position.x && System.Math.Abs(transform.position.x - Camera.main.transform.position.x) > 17)
            if (inputX > 0)
                inputX = 0;

        if (transform.position.y < Camera.main.transform.position.y && System.Math.Abs(transform.position.y - Camera.main.transform.position.y) > 11)
            if (inputY < 0)
                inputY = 0;

        if (transform.position.y > Camera.main.transform.position.y && System.Math.Abs(transform.position.y - Camera.main.transform.position.y) > 12)
            if (inputY > 0)
                inputY = 0;


        movement = new Vector2(
            speed.x * inputX,
            speed.y * inputY);
        //restartButton.SetActive(true);
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
            if(playerHealth.hp == 1)
                restartButton.SetActive(true);

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
    
    void OnDestroy()
    {
        restartButton.SetActive(true);
        //RestartScript restart = restartButton.GetComponent<RestartScript>();
        //restart.Enable();
    }
}
