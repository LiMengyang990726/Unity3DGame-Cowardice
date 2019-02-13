using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    private PlayerDialogue playerDialogue;
    public void OnCollision(Collider other, Collision collision)
    {
        if ((other.gameObject.CompareTag("Player")) && (collision.GetType() == typeof(CapsuleCollider)))
        {
            Debug.Log("CapsuleCollider triggered");
            //playerDialogue.BoxDialogue();
        }
    }
}
