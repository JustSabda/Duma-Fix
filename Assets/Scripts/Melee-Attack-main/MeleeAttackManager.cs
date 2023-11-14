using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackManager : Character
{
    //How much the player should move either downwards or horizontally when melee attack collides with a GameObject that has EnemyHealth script on it
    public float defaultForce = 300;
    //How much the player should move upwards when melee attack collides with a GameObject that has EnemyHealth script on it
    public float upwardsForce = 600;
    //How long the player should move when melee attack collides with a GameObject that has EnemyHealth script on it
    public float movementTime = .1f;
    //Input detection to see if the button to perform a melee attack has been pressed
    [HideInInspector]public bool meleeAttack;
    //The animator on the meleePrefab
    private Animator meleeAnimator;

    [SerializeField]
    private float delayAttack = 0.2f;

    private float timer = 0f;

    public bool canAttack;

    [HideInInspector]public bool animSlash;
    private PlayerMovement player;
    /*
    //The Animator component on the player
    private Animator anim;
    //The Character script on the player; this script on my project manages the grounded state, so if you have a different script for that reference that script
    

    //Run this method instead of Initialization if you don't have any scripts inheriting from each other
    */
    private void Start()
    {
        //The Animator component on the player
        //anim = GetComponent<Animator>();
        //The Character script on the player; this script on my project manages the grounded state, so if you have a different script for that reference that script
        //character = GetComponent<Character>();
        //The animator on the meleePrefab
        meleeAnimator = GetComponentInChildren<MeleeWeapon>().gameObject.GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
        canAttack = true;
        animSlash = false;
    }
    

    //Start method from the Character script; grabs all the references the script has and adds some; if you don't inherit from a parent Character script, delete or comment out this method and use Start instead
    //protected override void Initializtion()
    //{
    //    //This grabs all the references already defined by the Character script
    //    base.Initializtion();
    //    //The animator on the meleePrefab
    //    meleeAnimator = GetComponentInChildren<MeleeWeapon>().gameObject.GetComponent<Animator>();
    //    canAttack = true;
    //}


    private void Update()
    {
        //Method that checks to see what keys are being pressed
        if(player.isDead == false)
        CheckInput();
    }

    private void CheckInput()
    {
        if (!canAttack)
        {
            timer += Time.deltaTime;
            if(timer>= delayAttack)
            {
                timer = 0;
                canAttack = true;
                animSlash = false;
            }
        }


        //Checks to see if Backspace key is pressed which I define as melee attack; you can of course change this to anything you would want
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //Sets the meleeAttack bool to true
            Attack();
        }

        //Checks to see if meleeAttack is true and pressing up
        if (meleeAttack && Input.GetAxis("Vertical") > 0)
        {
            //Turns on the animation for the player to perform an upward melee attack
            //anim.SetTrigger("UpwardMelee");
            //Turns on the animation on the melee weapon to show the swipe area for the melee attack upwards
   
            
            meleeAttack = false;
            canAttack = false;

            meleeAnimator.SetTrigger("UpwardMeleeSwipe");
            animSlash = true;
        }
        //Checks to see if meleeAttack is true and pressing down while also not grounded
        if (meleeAttack && Input.GetAxis("Vertical") < 0 && !character.isGrounded)
        {
            //Turns on the animation for the player to perform a downward melee attack
            //anim.SetTrigger("DownwardMelee");
            //Turns on the animation on the melee weapon to show the swipe area for the melee attack downwards


            meleeAttack = false;
            canAttack = false;

            meleeAnimator.SetTrigger("DownwardMeleeSwipe");

            animSlash = true;
        }
        //Checks to see if meleeAttack is true and not pressing any direction
        if ((meleeAttack && Input.GetAxis("Vertical") == 0)
            //OR if melee attack is true and pressing down while grounded
            || meleeAttack && (Input.GetAxis("Vertical") < 0 && character.isGrounded))
        {
            //Turns on the animation for the player to perform a forward melee attack
            //anim.SetTrigger("ForwardMelee");
            //Turns on the animation on the melee weapon to show the swipe area for the melee attack forwards


            meleeAttack = false;
            canAttack = false;

            meleeAnimator.SetTrigger("ForwardMeleeSwipe");
            animSlash = true;

        }
    }

    public void Attack()
    {
        if (canAttack)
        {
            AudioManager.Instance.PlaySFX("Attack SFX");
            meleeAttack = true;
        }
            
       
    }

    //public void AttackUp()
    //{
    //    meleeAttack = false;
    //}

    private IEnumerator DelayAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        meleeAttack = false;
    }
}
