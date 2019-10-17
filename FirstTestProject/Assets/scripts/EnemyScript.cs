using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private WeaponScript[] weapons;
    public static bool isTimeStopped = false;
    private static int timeCount;

    void Awake()
    {
        // Retrieve the weapon only once
        weapons = GetComponentsInChildren<WeaponScript>();

    }

    void Start()
    {
        //Debug.Log("Start");
        var rand = new System.Random();
        Vector3 movement = new Vector3(
          0,
          rand.Next(-2, 2),
          0);

        //transform.Translate(movement);
    }

    void Update()
    {

        foreach (WeaponScript weapon in weapons)
        {
            // Auto-fire
            if (weapon != null && weapon.CanAttack)
            {
                weapon.Attack(true);
                SoundEffectsHelper.Instance.MakeEnemyShotSound();
            }
        }

        if (transform.position.x < Camera.main.transform.position.x && System.Math.Abs(transform.position.x - Camera.main.transform.position.x) > 25)
            Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        // Destroy the enemy
        Destroy(gameObject);
    }
}
