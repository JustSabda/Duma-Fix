using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float maxSpeed;
    public bool chase = false;
    public Vector3 startingPoint;
    private float curSpeed;

    public int enemyDamage;
   
    public GameObject player;

    float startingY;
    public float rangePatrol;
    int dir = 1;
    public bool isPatrol;

    private Animator anim;

    [SerializeField]
    bool isAttack = false;
    [SerializeField] private float range;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;


    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    private Health playerHealth;

    private bool collide;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        anim = GetComponent<Animator>();
        startingPoint = transform.position;
        startingY = startingPoint.y;

        isPatrol = true;
        anim.SetBool("isRunning", true);
        curSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        collide =
    Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
    new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
    0, Vector2.right, 0, playerLayer);

        if (player == null)
            return;
        if (!isAttack)
        {
            if (chase == true)
                Chase();
            else
                ReturnStartPoint();
        }
        Flip();

        cooldownTimer += Time.deltaTime;
    }

    private void Chase()
    {
        startingPoint.y = transform.position.y;
        isPatrol = false;

        //if(Vector2.Distance(transform.position,player.transform.position)<= 0.5f )
        //{
        //    enemyAttack();
        //}
        if (collide)
        {
            enemyAttack();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, curSpeed * Time.deltaTime);
            anim.SetBool("isRunning", true);
        }
    }

    //public bool PlayerInSight()
    //{
    //    RaycastHit2D hit =
    //        Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
    //        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
    //        0, Vector2.right, 0 , playerLayer);


    //    if (hit.collider == GameObject.FindGameObjectWithTag("Player"))
    //    {
    //        playerHealth = hit.transform.GetComponent<Health>(); 

    //    }
    //        playerHealth = hit.transform.GetComponent<Health>();

    //    return hit.collider != null;
    //}

    void enemyAttack()
    {
        isAttack = true;
        curSpeed = 0;
        anim.SetTrigger("isAttack");

        StartCoroutine(WaitForAnimation(anim.GetCurrentAnimatorStateInfo(0).length));
    }

    private void DamagePlayer()
    {
        if (collide)
        {
            playerHealth.enemy = this.gameObject;
            playerHealth.Damage(enemyDamage);
        }
    }

    IEnumerator WaitForAnimation(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        isAttack = false;
        curSpeed = maxSpeed;
        anim.SetBool("isRunning", false);
    }
    private void ReturnStartPoint()
    {
        anim.SetBool("isRunning", true);
        if (!isPatrol)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, startingPoint, curSpeed * Time.deltaTime);

   
        }

        if (transform.position == startingPoint)
        {
            
            isPatrol = true;
        }



    }

    private void FixedUpdate()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (isPatrol)
        {
            transform.Translate(Vector2.up * curSpeed * Time.deltaTime * dir);
            if (transform.position.y < startingY - rangePatrol || transform.position.y > startingY + rangePatrol)
            {
                StartCoroutine(Idle());
                dir *= -1;
            }

        }
    }
    private IEnumerator Idle()
    {
        curSpeed = 0;
        anim.SetBool("isRunning", false);
        yield return new WaitForSeconds(2f);

        curSpeed = maxSpeed;
        anim.SetBool("isRunning", true);
    }


    

    private void Flip()
    {
        if (transform.position.x < player.transform.position.x || (!chase && transform.position.x < startingPoint.x))
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);


    }


    private void OnDrawGizmos()
    {
        if (collide)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;

        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
