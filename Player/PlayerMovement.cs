using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Player parameters
    public float playerCurrentSpeed;
    public float rotateSpeed = 200f;

    public float normalWalkSpeed = 2;
    public float normalSprintSpeed = 4;

    public float lowConfWalkSpeed = 1;
    public float lowConfSprintSpeed = 3;

    public float highConfWalkSpeed = 3;
    public float highConfSprintSpeed = 5;

    //Player Status
    public string currentConfidenceState; //Must be written as: "Low Confidence", "Normal Confidence", "High Confidence"

    //Attack amount
    public int lowConfiAttack = 5;
    public int normalConfiAttack = 10;
    public int highConfiAttack = 20;
    public int amount;
    public BigMonsterHealth bigMonsterHealth;

    public float degreesPerSecond = 1000;
    Quaternion targetRotation;

    Rigidbody playerRigidbody;
    Animator anim;
    Vector3 movement;
    Vector3 moveDirection;


    //Attack parameters
    string[] comboChain;
    float fireRate = 1;
    private int comboNumber = 0;
    private float resetTimer;

    //public GameObject[] bigMonsters;
    //public GameObject[] smallMonsters;
    //public GameObject closestBigMonster = null;
    //public GameObject closestSmallMonster = null;
    
    public GameObject bigMonster0;
    public GameObject bigMonster1;
    public GameObject bigMonster2;

    public GameObject smallMonster0;
    public GameObject smallMonster1;
    public GameObject smallMonster2;

    void Start()
    {
        //Get player references
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //bigMonsters = GameObject.FindGameObjectsWithTag("BigEnemy");
        //smallMonsters = GameObject.FindGameObjectsWithTag("SmallEnemy");

        //Initialise attack combo parameters
        if (comboChain == null || (comboChain != null && comboChain.Length == 0))
        {
            comboChain = new string[] { "Attack1", "Attack2", "Attack3" };
        }

        //Initialise player Confidence state
        currentConfidenceState = "Low Confidence";
        

    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        resetFromMovingToIdleAnimation();

        //Run timer
        resetTimer += Time.deltaTime;

        //<MEELEE ATTACK CODE>
        if (Input.GetMouseButton(2) /*Input.GetButtonDown("Fire1") */&& comboNumber < comboChain.Length)
        {

            //execute Combo attack animation (consequtive/chained)
            anim.SetTrigger(comboChain[comboNumber]);

            comboNumber = (comboNumber + 1) % comboChain.Length; //wraparound/loop combo chain attacks
            resetTimer = 0f; //start counting from 0... 

        }

        //Debug.Log("resetTimer = " + resetTimer);

        //After clicking, start timing -> if no more clicks after time = 2.0f, reset animation to IDLE
        if ((resetTimer > fireRate))
        {
            anim.SetTrigger("Reset");
            comboNumber = 0;
            resetAttackTrigger();


        }

        //<MOVEMENT CODE>
        
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("IsWalking", false);
                playerCurrentSpeed = getSpeedBasedOnConfidenceLevel(currentConfidenceState, "sprint");
                
                if (!playerIsAttacking())
                {
                    sprintingAnimation();
                    transform.position += transform.forward * Time.deltaTime * playerCurrentSpeed;
                }
            }
            else
            {
                playerCurrentSpeed = getSpeedBasedOnConfidenceLevel(currentConfidenceState, "walk");
                
                if (!playerIsAttacking())
                {
                    walkingAnimation(h, v);
                    transform.position += transform.forward * Time.deltaTime * playerCurrentSpeed;
                }
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("IsWalking", false);
                playerCurrentSpeed = getSpeedBasedOnConfidenceLevel(currentConfidenceState, "sprint");
                
                if (!playerIsAttacking())
                {
                    sprintingAnimation();
                    transform.position -= transform.forward * Time.deltaTime * playerCurrentSpeed;
                }
            }
            else
            {
                playerCurrentSpeed = getSpeedBasedOnConfidenceLevel(currentConfidenceState, "walk");
                
                if (!playerIsAttacking())
                {
                    walkingAnimation(h, v);
                    transform.position -= transform.forward * Time.deltaTime * playerCurrentSpeed;
                }
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("IsWalking", false);
                playerCurrentSpeed = getSpeedBasedOnConfidenceLevel(currentConfidenceState, "sprint");
                
                if (!playerIsAttacking())
                { 
                sprintingAnimation();
                transform.position -= transform.right * Time.deltaTime * playerCurrentSpeed;
                }
            }
            else
            {
                playerCurrentSpeed = getSpeedBasedOnConfidenceLevel(currentConfidenceState, "walk");
                
                if (!playerIsAttacking())
                {
                    walkingAnimation(h, v);
                    transform.position -= transform.right * Time.deltaTime * playerCurrentSpeed;
                }
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("IsWalking", false);
                playerCurrentSpeed = getSpeedBasedOnConfidenceLevel(currentConfidenceState, "sprint");
                
                if (!playerIsAttacking())
                {
                    sprintingAnimation();
                    transform.position += transform.right * Time.deltaTime * playerCurrentSpeed;
                }
            }
            else
            {
                playerCurrentSpeed = getSpeedBasedOnConfidenceLevel(currentConfidenceState, "walk");
                
                if (!playerIsAttacking())
                {
                    walkingAnimation(h, v);
                    transform.position += transform.right * Time.deltaTime * playerCurrentSpeed;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            //playerRigidbody.velocity = new Vector3(0, 10, 0);
            playerRigidbody.AddForce(0, 1000, 0);
            Debug.Log("Jump");
        }

        /*
        //cannot move/rotate while displaying any attacking animation
        if (!(anim.GetCurrentAnimatorStateInfo(0).IsName("2Hand-Sword-Attack1")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("2Hand-Sword-Attack4")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("2Hand-Sword-Attack6")
            ))

        {

            //Move player
            Move(h, v, playerCurrentSpeed);

            //Rotate();
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            LookAtDirection(moveDirection);
        }
        */

        //playerRigidbody.MovePosition(new Vector3(0, -3, 0));
        //Hit();
    }

    float getSpeedBasedOnConfidenceLevel(string confidenceLevel, string movementType)
    {

        if (confidenceLevel == "Low Confidence")
        {
            if (movementType == "walk")
            {
                return lowConfWalkSpeed;
            }
            else if (movementType == "sprint")
            {
                return lowConfSprintSpeed;
            }
            else
                Debug.Log(movementType + " doesn't exist!");

        }

        else if (confidenceLevel == "Normal Confidence")
        {
            if (movementType == "walk")
            {
                return normalWalkSpeed;
            }
            else if (movementType == "sprint")
            {
                return normalSprintSpeed;
            }
            else
            {
                Debug.Log(movementType + " doesn't exist!");
            }
        }
        else if (confidenceLevel == "High Confidence")
        {
            if (movementType == "walk")
            {
                return highConfWalkSpeed;
            }
            else if (movementType == "sprint")
            {
                return highConfSprintSpeed;
            }
            else
            {
                Debug.Log(movementType + " doesn't exist!");
            }
        }

        else
        {
            Debug.LogError("confidenceLevel specified does not exist!");
        }
        return 0;

    }

    bool playerIsAttacking()
    {
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("2Hand-Sword-Attack1")
    || anim.GetCurrentAnimatorStateInfo(0).IsName("2Hand-Sword-Attack4")
    || anim.GetCurrentAnimatorStateInfo(0).IsName("2Hand-Sword-Attack6")
    ))
        {
            
            return true;
        }
        else
        {
            return false;
        }
    }


    void Move(float h, float v, float playerSpeed)
    {

        movement.Set(h, 0f, v);

        movement = movement.normalized * playerSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);


    }


    void LookAtDirection(Vector3 moveDirection)
    {
        Vector3 xzDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
        if (xzDirection.magnitude > 0)
            targetRotation = Quaternion.LookRotation(xzDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,
            targetRotation, degreesPerSecond * Time.deltaTime);
    }

    void resetFromMovingToIdleAnimation()
    {
        anim.SetBool("IsSprinting", false);
        anim.SetBool("IsWalking", false);

    }

    void walkingAnimation(float h, float v)
    {
        //if is moving, 'default' = walk
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }

    void resetAttackTrigger()
    {
        anim.ResetTrigger("Attack1");
        anim.ResetTrigger("Attack2");
        anim.ResetTrigger("Attack3");
    }

    void sprintingAnimation()
    {
        anim.SetBool("IsSprinting", true);
    }

    void Hit()/*public void OnTriggerEnter(Collider other)*/
    {

        //find the closest small monster 
        //float distance = Mathf.Infinity;
        //Vector3 position = transform.position;
        //foreach (GameObject smallMonster in smallMonsters)
        //{
        //    Vector3 diff = smallMonster.transform.position - position;
        //    float curDistance = diff.sqrMagnitude;
        //    if (curDistance < distance)
        //    {
        //       closestSmallMonster = smallMonster;
        //    }
        //}

        // find the closest big monster
        //foreach (GameObject bigMonster in bigMonsters)
        //{
        //    Vector3 diff = bigMonster.transform.position - position;
        //    float curDistance = diff.sqrMagnitude;
        //    if (curDistance < distance)
        //    {
        //        closestBigMonster = bigMonster;
        //    }
        //}

        // detect if in the attacking stages
        bool attack = anim.GetBool("Attack1") || anim.GetBool("Attack2") || anim.GetBool("Attack3");

        if ((Vector3.Distance(transform.position,bigMonster0.transform.position)<=6f) && (attack))
        {
            //Debug.Log("Here is executed");
            BigMonsterHealth bigMonsterHealth = bigMonster0.GetComponent<BigMonsterHealth>();
            if (currentConfidenceState == "Low Confidence")
            {
                amount = lowConfiAttack;
            }
            else if (currentConfidenceState == "Normal Confidence")
            {
                amount = normalConfiAttack;
            }
            else
            {
                amount = normalConfiAttack;
            }
            bigMonsterHealth.TakeDamage(amount);
        }

        if ((Vector3.Distance(bigMonster1.transform.position, transform.position) <= 3f) && (attack))
        {
            BigMonsterHealth bigMonsterHealth = bigMonster1.GetComponent<BigMonsterHealth>();
            if (currentConfidenceState == "Low Confidence")
            {
                amount = lowConfiAttack;
            }
            else if (currentConfidenceState == "Normal Confidence")
            {
                amount = normalConfiAttack;
            }
            else
            {
                amount = normalConfiAttack;
            }
            Debug.Log("Ruth is taking damage");
            bigMonsterHealth.TakeDamage(amount);
        }

        if ((Vector3.Distance(bigMonster2.transform.position, transform.position) <= 3f) && (attack))
        {
            BigMonsterHealth bigMonsterHealth = bigMonster2.GetComponent<BigMonsterHealth>();
            if (currentConfidenceState == "Low Confidence")
            {
                amount = lowConfiAttack;
            }
            else if (currentConfidenceState == "Normal Confidence")
            {
                amount = normalConfiAttack;
            }
            else
            {
                amount = normalConfiAttack;
            }
            Debug.Log("Ruth is taking damage");
            bigMonsterHealth.TakeDamage(amount);
        }

        if ((Vector3.Distance(smallMonster0.transform.position, transform.position) <= 3f) && (attack))
        {
            SmallMonsterHealth smallMonsterHealth = smallMonster0.GetComponent<SmallMonsterHealth>();
            if (currentConfidenceState == "Low Confidence")
            {
                amount = lowConfiAttack;
            }
            else if (currentConfidenceState == "Normal Confidence")
            {
                amount = normalConfiAttack;
            }
            else
            {
                amount = normalConfiAttack;
            }
            Debug.Log("Ruth is taking damage");
            smallMonsterHealth.TakeDamage(25);
        }

        if ((Vector3.Distance(smallMonster1.transform.position, transform.position) <= 3f) && (attack))
        {
            SmallMonsterHealth smallMonsterHealth = smallMonster1.GetComponent<SmallMonsterHealth>();
            if (currentConfidenceState == "Low Confidence")
            {
                amount = lowConfiAttack;
            }
            else if (currentConfidenceState == "Normal Confidence")
            {
                amount = normalConfiAttack;
            }
            else
            {
                amount = normalConfiAttack;
            }
            Debug.Log("Ruth is taking damage");
            smallMonsterHealth.TakeDamage(25);
        }

        if ((Vector3.Distance(smallMonster2.transform.position, transform.position) <= 3f) && (attack))
        {
            SmallMonsterHealth smallMonsterHealth = smallMonster2.GetComponent<SmallMonsterHealth>();
            if (currentConfidenceState == "Low Confidence")
            {
                amount = lowConfiAttack;
            }
            else if (currentConfidenceState == "Normal Confidence")
            {
                amount = normalConfiAttack;
            }
            else
            {
                amount = normalConfiAttack;
            }
            Debug.Log("Ruth is taking damage");
            smallMonsterHealth.TakeDamage(25);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

}
