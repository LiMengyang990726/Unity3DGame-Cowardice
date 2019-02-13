using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMonsterController : MonoBehaviour
{
    public bool isDead;
    public bool inCombat;
    public float wanderTime;
    public float movementSpeed;
    public float chaseSpeed;

    public GameObject player;
    public bool playerWithInFollowRange;
    public bool playerWithInAttackRange;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        playerWithInAttackRange = false;
        playerWithInFollowRange = false;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerWithInFollowRange = (Vector3.Distance(transform.position, player.transform.position) <= 10f);
        playerWithInAttackRange = (Vector3.Distance(transform.position, player.transform.position) <= 2f);

        if (!isDead)
        {
            if((wanderTime > 0) && (!playerWithInFollowRange))
            {
                transform.Translate(Vector3.forward * movementSpeed);
                wanderTime -= Time.deltaTime;
            }
            else if(!playerWithInFollowRange)
            {
                wanderTime = Random.Range(5.0f, 15.0f);
                Wander();
            }
            else if((playerWithInFollowRange) && (playerWithInAttackRange))
            {
                anim.SetBool("PlayerWithInFollowRange", true);
                transform.LookAt(player.transform);
                transform.position += transform.forward * chaseSpeed * Time.deltaTime;
            }
            else
            {
                anim.SetBool("PlayerWithInAttackRange", true);
                //Attack();
            }
        }
    }

    void Wander()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Attack();
        }
        else
        {
            Wander();
        }
    }

    void Attack()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(5);
        PlayerConfidence playerConfidence = player.GetComponent<PlayerConfidence>();
        playerConfidence.decreaseConfidenceLevel(5);
    }
}
