using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 100;
    public float currentHealth;

    public Slider healthSlider;
    public Image damageImage;

    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    public PlayerDialogue playerDialogue;

    Animator anim; //for death anim
    PlayerMovement playerMovement;//check

    bool isDead;
    bool damaged;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("IsAttacked"))
        {
            TakeDamage(80*Time.deltaTime);
        }


        if (damaged)
        {
            damageImage.color = flashColour;
        }

        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    public void TakeDamage(float amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        //Check if player is dead. Then handle death events
        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
        if(currentHealth <= 20)
        {
            playerDialogue.LowHealth();
        }
    }

    public void AddHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        healthSlider.value = currentHealth;
    }

    void Death()
    {
        isDead = true;

        anim.SetTrigger("Die");
        
    }
}
