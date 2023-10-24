using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    public float curSpeed;


    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;

    private bool collideRight;

    [Header("Enemy Attack")]
    private bool collideAttack;
    bool isAttack = false;
    public int enemyDamage;
    private Health playerHealth;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private BoxCollider2D boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        isChasing = false;
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;

        anim.SetBool("isRunning", true);
        curSpeed = speed;
        

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        
        collideRight = Physics2D.Raycast(transform.position + new Vector3(0, 0, 0), transform.right, chaseDistance, LayerMask.GetMask("Player"));

        collideAttack =
    Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
    new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
    0, Vector2.right, 0, playerLayer);

        if (collideRight)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (!isAttack)
        {
            if (isChasing)
            {
                if (!collideAttack)
                {
                    if (transform.position.x < playerTransform.position.x)
                    {
                        if (curSpeed != 0)
                            rb.velocity = new Vector2(curSpeed + 1, 0);
                    }
                    else
                    {
                        if (curSpeed != 0)
                            rb.velocity = new Vector2(-curSpeed - 1, 0);
                    }
                }
                else
                {
                    enemyAttack();
                }
            }
            else
            {

                if (currentPoint == pointB.transform)
                {
                    rb.velocity = new Vector2(curSpeed, 0);
                    //anim.SetBool("isRunning", true);
                }
                else
                {
                    rb.velocity = new Vector2(-curSpeed, 0);
                    //anim.SetBool("isRunning", true);
                }


                if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
                {
                    StartCoroutine(Idle());
                    currentPoint = pointA.transform;
                }
                else if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
                {
                    StartCoroutine(Idle());
                    currentPoint = pointB.transform;
                }
            }
        }
    }

    private IEnumerator Idle()
    {
        curSpeed = 0;
        anim.SetBool("isRunning", false);
        yield return new WaitForSeconds(2f);

        flip();
        curSpeed = speed;
    }

    private void flip()
    {
        anim.SetBool("isRunning", true);
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        chaseDistance *= -1;
        transform.localScale = localScale;
    }

    void enemyAttack()
    {
        isAttack = true;
        curSpeed = 0;
        anim.SetTrigger("isAttack");

        StartCoroutine(WaitForAnimation(anim.GetCurrentAnimatorStateInfo(0).length));
    }

    private void DamagePlayer()
    {
        if (collideAttack)
        {
            playerHealth.enemy = this.gameObject;
            playerHealth.Damage(enemyDamage);
        }
    }

    IEnumerator WaitForAnimation(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        isAttack = false;
        curSpeed = speed;
        anim.SetBool("isRunning", true);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);

        if (collideRight)
            Debug.DrawRay(transform.position + new Vector3(0, 0, 0), transform.right * chaseDistance, Color.red);
        else
            Debug.DrawRay(transform.position + new Vector3(0, 0, 0), transform.right * chaseDistance, Color.white);

    }

    private void OnDrawGizmosSelected()
    {
        if (collideAttack)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;

        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    //public void IsDead()
    //{
    //    anim.SetTrigger("isDead");

    //    gameObject.SetActive(false);
    //}



}
