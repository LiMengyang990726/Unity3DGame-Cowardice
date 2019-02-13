using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConfidence : MonoBehaviour
{

    public int startingConfidence = 50;
    public int currentConfidence;
    public PlayerDialogue playerDialogue;
    public Slider confidenceBar;
    GameObject ruth;
    private Animator anim;

    PlayerMovement playerMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        ruth = GameObject.FindWithTag("Player");
        currentConfidence = startingConfidence;
        //confidenceBar = GetComponent<Slider>();
        confidenceBar.value = currentConfidence;

        anim = GetComponent<Animator>();

        playerMovementScript = ruth.GetComponent<PlayerMovement>();
        playerDialogue = ruth.GetComponent<PlayerDialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        if (0 <= currentConfidence && currentConfidence <= 25)
        {
            Debug.Log("Entered Low Confidence state");
            enterLowConfidenceState();
        }

        else if (currentConfidence >= 75 && currentConfidence <= 100)
        {
            Debug.Log("Entered High Confidence state");
            enterHighConfidenceState();
        }

        else
        {
            Debug.Log("Entered Normal Confidence state");
            enterNormalConfidenceState();
        }

        //update confidence level
        confidenceBar.value = currentConfidence;


    }

    public void decreaseConfidenceLevel (int amount)
    {
        currentConfidence -= amount;
    }

    public void increaseConfidenceLevel(int amount)
    {
        currentConfidence += amount;
    }

    void enterHighConfidenceState()
    {
        playerMovementScript.currentConfidenceState = "High Confidence";
        playerDialogue.HighConfidenceDialogue();
    }

    void enterNormalConfidenceState()
    {
        playerMovementScript.currentConfidenceState = "Normal Confidence";
    }

    void enterLowConfidenceState()
    {
        playerMovementScript.currentConfidenceState = "Low Confidence";
        playerDialogue.LowConfidenceDialogue();
    }

}
