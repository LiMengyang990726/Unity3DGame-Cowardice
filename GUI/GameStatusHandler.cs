using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusHandler : MonoBehaviour
{
    Animator anim;
    PlayerHealth playerHealth;
    GuiTimer guiTimer;

    GameObject HUDCanvas;
    GameObject player;
    // Start is called before the first frame update

    private bool looseTimeDisplayedBefore = false;
    private DisplayTimeTakenAfterLosing displayTimeTakenAfterLosing;

    void Start()
    {
        anim = GetComponent<Animator>();

        player = GameObject.Find("Ruth");

        playerHealth = player.GetComponent<PlayerHealth>();


        HUDCanvas = GameObject.FindGameObjectWithTag("GUI");
        guiTimer = HUDCanvas.transform.Find("Timer").GetComponent<GuiTimer>();
        

        displayTimeTakenAfterLosing = HUDCanvas.transform.Find("LooseTimeTaken").GetComponent<DisplayTimeTakenAfterLosing>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((playerHealth.currentHealth <= 0) && !looseTimeDisplayedBefore)
        {
            anim.SetTrigger("Lose");

            displayTimeTakenAfterLosing.updateTimeText(guiTimer.getTimeInHMSFormat());
            looseTimeDisplayedBefore = true;
        }
    }


}
