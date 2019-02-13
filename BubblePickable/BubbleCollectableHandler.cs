using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollectableHandler : MonoBehaviour
{
    public int amtToIncreaseBy = 10;

    private GameObject player;
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Ruth");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided!");
            playerHealth.AddHealth(amtToIncreaseBy);

            gameObject.SetActive(false);
        }
    }
}
