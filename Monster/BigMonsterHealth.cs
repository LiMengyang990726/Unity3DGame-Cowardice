using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMonsterHealth : MonoBehaviour
{
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public int scoreValue = 20;                 // The amount added to the player's score when the enemy dies.
    public static int cumulativeAmount = 0;            // When it can divide 20, do defend

    Animator anim;                              // Reference to the animator.
    CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
    bool isDead;                                // Whether the enemy is dead.
    
    //to be disabled after death
    public GameObject Ignit;
    public ParticleSystem Ignition;
    public GameObject Flame;
    public ParticleSystem Flames;
    public GameObject Light;
    public ParticleSystem Lights;
    
    private MonsterController monsterController;
    public PlayerConfidence playerConfidence;
    public GameObject player;
    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        monsterController = GetComponent<MonsterController>();
        playerConfidence = player.GetComponent<PlayerConfidence>();

        // Setting the current health when the enemy first spawns.
        //currentHealth = startingHealth;
        currentHealth = 100;

        Ignit = this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject;
        Ignition = Ignit.GetComponent<ParticleSystem>();
        Flame = this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(2).gameObject;
        Flames = Flame.GetComponent<ParticleSystem>();
        Light = this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(3).gameObject;
        Lights = Light.GetComponent<ParticleSystem>();
    }
    

    public void TakeDamage(int amount)
    {
        if (isDead)
        {
           //do nothing
        }
        else if(currentHealth - amount <= 0)
        {
            Death();
        }
        else
        {
            currentHealth -= amount;
            cumulativeAmount += amount;
            //Debug.Log("Attacked");
            if(cumulativeAmount % 20 == 0)
            {
                anim.SetBool("Attacked", true);
                Debug.Log("Defending");
                new WaitForSeconds(5);
                anim.SetBool("Attacked", false);
            }
            else if(currentHealth <= 20)
            {
                anim.SetTrigger("LowHealth");
            }
        }
    }
    

    void Death()
    {
        isDead = true;
        
        anim.SetTrigger("Dead");

        Ignition.Stop();
        Flames.Stop();
        Lights.Stop();
        monsterController.isDead = true;

        playerConfidence.increaseConfidenceLevel(scoreValue);
    }
}