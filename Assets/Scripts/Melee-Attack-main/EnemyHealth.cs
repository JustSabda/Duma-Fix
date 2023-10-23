using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //Determines if this GameObject should receive damage or not
    [SerializeField]
    private bool damageable = true;
    //The total number of health points the GameObject should have
    [SerializeField]
    private int healthAmount = 100;
    //The max amount of time after receiving damage that the enemy can no longer receive damage; this is to help prevent the same melee attack dealing damage multiple times
    [SerializeField]
    private float invulnerabilityTime = .2f;
    //Allows the player to be forced up when performing a downward strike above the enemy
    public bool giveUpwardForce = true;
    //Bool that manages if the enemy can receive more damage
    private bool hit;
    //The current amount after receiving damage the enemy has
    [SerializeField]
    private int currentHealth;

    public enum Enemy {Land, Fly};
    public Enemy enemyType;


    private EnemyPatrol enemy1;
    private FlyingEnemy enemy2;

    private void Start()
    {
        //Sets the enemy to the max amount of health when the scene loads
        currentHealth = healthAmount;
        
        if(enemyType == Enemy.Land)
        {
            enemy1 = GetComponent<EnemyPatrol>();
        }
        else
        {
            enemy2 = GetComponent<FlyingEnemy>();
        }
    }

    private void Update()
    {

    }
    public void Damage(int amount)
    {
        //First checks to see if the player is currently in an invulnerable state; if not it runs the following logic.
        if (damageable && !hit && currentHealth > 0)
        {
            //First sets hit to true
            hit = true;
            //Reduces currentHealthPoints by the amount value that was set by whatever script called this method, for this tutorial in the OnTriggerEnter2D() method
            currentHealth -= amount;
            //If currentHealthPoints is below zero, player is dead, and then we handle all the logic to manage the dead state
            if (currentHealth <= 0)
            {
                //Caps currentHealth to 0 for cleaner code
                currentHealth = 0;
                //Removes GameObject from the scene; this should probably play a dying animation in a method that would handle all the other death logic, but for the test it just disables it from the scene
                gameObject.SetActive(false);
            }
            else
            {
                //Coroutine that runs to allow the enemy to receive damage again
                StartCoroutine(TurnOffHit());
            }
        }
    }

    //Coroutine that runs to allow the enemy to receive damage again
    private IEnumerator TurnOffHit()
    {
        if (enemyType == Enemy.Land)
        {
            
            enemy1.curSpeed = 0;
        }
        else
        {
            enemy2.curSpeed = 0;
        }

        
        //Wait in the amount of invulnerabilityTime, which by default is .2 seconds
        yield return new WaitForSeconds(invulnerabilityTime);
        //Turn off the hit bool so the enemy can receive damage again
        hit = false;
        if (enemyType == Enemy.Land)
        {

            enemy1.curSpeed = enemy1.speed;
        }
        else
        {
            enemy2.curSpeed = enemy2.maxSpeed;
        }
        
    }
}
