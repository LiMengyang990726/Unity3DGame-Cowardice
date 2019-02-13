using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMonsterSimpleController : MonoBehaviour
{
    public bool PlayerWithInFollowRange;
    public bool isDead;
    public float moveSpeed;
    public bool PlayerWithInAttackRange;
    public GameObject player;
    public Animator m_Animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        m_Animator = GetComponent<Animator>();
        PlayerWithInFollowRange = false;
        PlayerWithInAttackRange = false;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerWithInFollowRange = Vector3.Distance(player.transform.position, transform.position) <= 50f;

        PlayerWithInAttackRange = Vector3.Distance(player.transform.position, transform.position) <= 20f;

        if (!isDead)                                                                        // not dead
        {
            if ((PlayerWithInFollowRange == true) && (PlayerWithInAttackRange == false))                                       // not dead, player in follow range
            {
                m_Animator.SetBool("PlayerWithInAttackRange", false);
                transform.LookAt(player.transform);
                transform.position = player.transform.position * moveSpeed * Time.deltaTime;
            }
            else if ((PlayerWithInFollowRange == true) && (PlayerWithInAttackRange == true))
            {
                m_Animator.SetBool("PlayerWithInAttackRange", true);                        // fire attack movement
                
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Attack();
        }
    }

    void Attack()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        playerHealth.TakeDamage(10);
    }
}
