using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIBig : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private Image healthBar;
    private BigMonsterHealth bigMonsterHealth;

    //private GameObject Blue;

    // Start is called before the first frame update
    void Start()
    {
        bigMonsterHealth = GetComponent<BigMonsterHealth>();
        //Blue = GameObject.Find("Blue");

        healthBar = gameObject.transform.Find("EnemyCanvas").transform.Find("MonsterHealthBG").transform.Find("MonsterHealth").GetComponent<Image>();//("Blue/EnemyCanvas/MonsterHealthBG/MonsterHealth").GetComponent<Image>();
        //healthBar = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = bigMonsterHealth.currentHealth;
        //Debug.Log(currentHealth);
        healthBar.fillAmount = (float)currentHealth/(float)maxHealth;
    }
}
