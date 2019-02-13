using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    // For following
    public bool PlayerWithInFollowRange;
    public bool isDead;
    public float wanderTime;
    public float wanderSpeed;
    public float moveSpeed;

    // For flame attack
    public GameObject Ignit;
    public ParticleSystem Ignition;
    public GameObject Flame;
    public ParticleSystem Flames;
    public GameObject Light;
    public ParticleSystem Lights;
    
    public Animator m_Animator;
    public bool PlayerWithInAttackRange;
    public GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerWithInFollowRange = false;
        isDead = false;

        Ignit = this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject;
        Ignition = Ignit.GetComponent<ParticleSystem>();
        Flame = this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(2).gameObject;
        Flames = Flame.GetComponent<ParticleSystem>();
        Light = this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(3).gameObject;
        Lights = Light.GetComponent<ParticleSystem>();

        m_Animator = GetComponent<Animator>();
        PlayerWithInAttackRange = false;
    }


    void Update()
    {
        PlayerWithInFollowRange = Vector3.Distance(player.transform.position, transform.position) <= 50f;

        PlayerWithInAttackRange = Vector3.Distance(player.transform.position, transform.position) <= 20f;

       
        //if (!isDead)                                                                        // not dead
        //{
        //    if ((wanderTime > 0)&&(PlayerWithInFollowRange == false))                       // not dead, player not in range, wander towards one direction
        //    {
        //        m_Animator.SetBool("PlayerWithInAttackRange", false);
        //        transform.Translate(Vector3.forward * wanderSpeed);
        //        wanderTime -= Time.deltaTime;
        //    }
        //    else if((wanderTime <= 0)&&(PlayerWithInFollowRange == false))                                      // not dead, player not in range, wander towards the opposite direction
        //    {
        //        m_Animator.SetBool("PlayerWithInAttackRange", false);
        //        wanderTime = Random.Range(5.0f, 15.0f);
        //        Wander();
        //    }
        //    else if((PlayerWithInFollowRange == true)&&(PlayerWithInAttackRange == false))                                       // not dead, player in follow range
        //    {
        //        m_Animator.SetBool("PlayerWithInAttackRange", false);
        //        transform.LookAt(player.transform);
        //        transform.position = player.transform.position * moveSpeed * Time.deltaTime;
        //    }
        //   else if((PlayerWithInFollowRange == true)&&(PlayerWithInAttackRange == true))
        //    {
        //        m_Animator.SetBool("PlayerWithInAttackRange", true);                        // fire attack movement
        //        Ignition.Emit(5);                                                           // fire attack particle systems
        //        Flames.Emit(5);
        //        Lights.Emit(5);
        //        Attack();
        //    }
        //}
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
                Ignition.Emit(5);                                                           // fire attack particle systems
                Flames.Emit(5);
                Lights.Emit(5);
                Attack();
            }
        }
    }

    void Wander()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
    }

    void Attack()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        //fireAudio.Play();
    }
}