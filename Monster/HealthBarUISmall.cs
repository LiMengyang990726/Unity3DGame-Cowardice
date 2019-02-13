using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUISmall : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    private Image healthBar;
    private SmallMonsterHealth smallMonsterHealth;

    //private GameObject BlueBoar;

    // Start is called before the first frame update
    void Start()
    {
        smallMonsterHealth = GetComponent<SmallMonsterHealth>();
        //BlueBoar = GameObject.Find("BlueBoar");

        healthBar = gameObject.transform.Find("EnemyCanvas").transform.Find("MonsterHealthBG").transform.Find("MonsterHealth").GetComponent<Image>();//("Blue/EnemyCanvas/MonsterHealthBG/MonsterHealth").GetComponent<Image>();
        //healthBar = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = smallMonsterHealth.currentHealth;
        //Debug.Log(currentHealth);
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }
}
