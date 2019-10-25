using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    /// <summary>
    /// Total hitpoints
    /// </summary>
    public int hp = 1;

    /// <summary>
    /// Enemy or player?
    /// </summary>
    public bool isEnemy = true;

    /// <summary>
    /// Inflicts damage and check if the object should be destroyed
    /// </summary>
    /// <param name="damageCount"></param>
    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if (hp <= 0)
        {
            SpecialEffectsHelper.Instance.Explosion(transform.position);
            SoundEffectsHelper.Instance.MakeExplosionSound();
            // Dead!
            PlayerScript player = GetComponent<PlayerScript>();
            if(player != null)
            {
                //GameObject restartObject = GameObject.FindWithTag("RestartButton");
                //if(restartObject != null)
                //    RestartScript restart = restartObject.GetComponent<RestartScript>();
                //if(restart != null)
                //    restart.Enable();
                RestartScript.active = true;
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {

        //is this a heart?
        LifeItemScript lifeItem = otherCollider.gameObject.GetComponent<LifeItemScript>();
        if(lifeItem != null)
        {
            if(!isEnemy)
            {
                hp++;
                Destroy(otherCollider.gameObject);
            }
        }

        // Is this a shot?
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            // Avoid friendly fire
            if (shot.isEnemyShot != isEnemy)
            {
                Damage(shot.damage);

                // Destroy the shot
                Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
                if(!shot.isEnemyShot)
                    ScoreScript.scoreValue += 10;
            }
            return;
        }

        HomingShotScript homingShot = otherCollider.gameObject.GetComponent<HomingShotScript>();
        if (homingShot != null)
        {
            Debug.Log("homing shot hit!");
            // Avoid friendly fire
            if (isEnemy)
            {
                Damage(homingShot.damage);

                // Destroy the shot
                Destroy(homingShot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
                ScoreScript.scoreValue += 10;
            }
            return;
        }
        else
            Debug.Log("homing shot not hit!");


        //is this an item?
        TimeStopScript item = otherCollider.gameObject.GetComponent<TimeStopScript>();

        if(item != null && isEnemy == false)
        {
            item.StopTime();
            Destroy(item.gameObject);
            return;
        }
    }
}
