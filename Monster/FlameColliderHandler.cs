using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameColliderHandler : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        PlayerConfidence playerConfidence = player.GetComponent<PlayerConfidence>();
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Flame collided w player!");
            playerHealth.TakeDamage(5);
            playerConfidence.decreaseConfidenceLevel(10);
        }
       
    }
}

