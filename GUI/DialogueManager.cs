using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text conversation;
    public GameObject Panel;

    void Awake()
    {
        RemoveAfterSeconds(20);
    }
    
    public void StartDialogue(string dialogue)
    {
        Debug.Log("Start conversation ");
        Show(dialogue);

        StartCoroutine(RemoveAfterSeconds(15));
    }

    void Show(string dialogue)
    {
        Panel.SetActive(true);
        conversation.gameObject.SetActive(true);
        //int currentDiaplayingText = 0;
        //for(int i = 0; i < (dialogue.Length + 1); i++)
        //{
        //    conversation.text = dialogue.Substring(0, i);
        //    new WaitForSeconds(0.02f);
        //}
        conversation.text = dialogue;
    }

    IEnumerator RemoveAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Panel.SetActive(false);
        conversation.gameObject.SetActive(false);
    }
}
