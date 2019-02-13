using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
    public GameObject[] bigMonsters;
    public GameObject[] smallMonsters;
    public GameObject closestSmallMonster;
    public GameObject closestBigMonster;
    public Transform box;
    public Transform door;
    public Transform bubble;
    public PlayerConfidence playerConfidence;
    public PlayerHealth playerHealth;
    //private bool[] flag = new bool[8];
    public string[] dialogue = new string[10];

    
    private void Start()
    {
        dialogue[0] = "Middle click to attack. Be careful...it can launch fire attack!";
        dialogue[1] = "Middle Click to attack. This enemy is not hard to defeat!";
        dialogue[2] = "I think this is the Pandora Box!";
        dialogue[3] = "Entered High confidence state! You are moving faster now.";
        dialogue[4] = "Entered Low confidence state! You are now moving slower. Kill monsters to increase confidence.";
        dialogue[5] = "Be careful! You are low on health.";
        dialogue[6] = "I think the Pandora box should be inside.";
        dialogue[7] = "Try touching bubbles, you will gain health";
        //for (int i = 0; i < flag.Length; i++) { flag[i] = true; }
        bigMonsters = GameObject.FindGameObjectsWithTag("BigEnemy");
        smallMonsters = GameObject.FindGameObjectsWithTag("SmallEnemy");
        box = GameObject.FindGameObjectWithTag("Goal").transform;
    }

    private void Update()
    {
        

        float bigDistance = Vector3.Distance(closestBigMonster.transform.position, transform.position);
        float smallDistance = Vector3.Distance(closestSmallMonster.transform.position, transform.position);

        if( (bigDistance <= 30f) /*&& (flag[0]) */)
        {
            Debug.Log("BigMonster is around here");
            FindObjectOfType<DialogueManager>().StartDialogue("Middle click to attack. Be careful...it can launch fire attack!");
            //flag[0] = false;
        }

        if ((smallDistance <= 10f)/* && (flag[1]) */)
        {
            Debug.Log("SmallMonster is around here");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue[1]);
            //flag[1] = false;
        }
        
        if( (Vector3.Distance(box.position, transform.position) <= 3f) /*&& (flag[2])*/)
        {
            Debug.Log("Box is around here");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue[2]);
            //flag[2] = false;
        }
        
        if((Vector3.Distance(door.position,transform.position) <= 3f) /*&& flag[3]*/)
        {
            FindObjectOfType<DialogueManager>().StartDialogue("I think the Pandora box should be inside.");
            //flag[3] = false;
        }

        if( (Vector3.Distance(bubble.position, transform.position) <= 3f) /*&& flag[4]*/)
        {
            Debug.Log("Bubble working");
            FindObjectOfType<DialogueManager>().StartDialogue("Try touching bubbles, you will gain health");
            //flag[4] = false;
        }

        if((playerConfidence.currentConfidence >= 75) /*&& flag[5] */)
        {
            HighConfidenceDialogue();
            //flag[5] = false;
        }
        if( (playerConfidence.currentConfidence <= 25) /*&& flag[6] */)
        {
            LowConfidenceDialogue();
            //flag[6] = false;
        }
        
        if((playerHealth.currentHealth <= 20) /*&& flag[7]*/)
        {
            LowHealth();
            //flag[7] = false;
        }
    }

    //public void MonsterDialogue()
    // {

    //}

    // public void BoxDialogue()
    //{
    //    Debug.Log("BoxDialogue Triggered");
    //    FindObjectOfType<DialogueManager>().StartDialogue(dialogue[2]);
    // }
    

    public void HighConfidenceDialogue()
    {
        
        FindObjectOfType<DialogueManager>().StartDialogue("Entered High confidence state! You are moving faster now.");
    }

    public void LowConfidenceDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue("Entered Low confidence state! You are now moving slower. Kill monsters to increase confidence.");
    }

    public void LowHealth()
    {
        FindObjectOfType<DialogueManager>().StartDialogue("Be careful! You are low on health.");
    }
}
