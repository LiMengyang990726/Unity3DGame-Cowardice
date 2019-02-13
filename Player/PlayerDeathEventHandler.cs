using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathEventHandler : MonoBehaviour
{
    Animator anim; //for death anim
    PlayerMovement playerMovement;//check

    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        
    }

    public void DeathHandler(string message)
    {
        Debug.Log("AlertObservers called");
        if (message.Equals("PlayerIsDead"))
        {
            
            anim.enabled = false;
            playerMovement.enabled = false;
            
        }
    }
}
