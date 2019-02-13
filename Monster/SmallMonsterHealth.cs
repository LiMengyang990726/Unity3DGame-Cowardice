using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMonsterHealth : MonoBehaviour
{
    public int startingHealth = 50;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.

    public Animator anim;
    public bool isDead;
    public GameObject smallMonster;
    public SmallMonsterController smallMonsterController;

    public GameObject player;
    public PlayerConfidence playerConfidence;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        smallMonsterController = smallMonster.GetComponent<SmallMonsterController>();
        playerConfidence = player.GetComponent<PlayerConfidence>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            //do nothing
        }
        else if (currentHealth - amount <= 0)
        {
            Death();
        }
        else
        {
            Debug.Log(currentHealth);
            currentHealth -= amount;
        }
    }

    public void Death()
    {
        isDead = true;
        

        anim.SetTrigger("Dead");
        
        smallMonsterController.isDead = true;

        playerConfidence.increaseConfidenceLevel(scoreValue);
    }
}
