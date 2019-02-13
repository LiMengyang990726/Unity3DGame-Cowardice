using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PickUpHandler : MonoBehaviour
{
    Animator anim;
    GameObject HUDCanvas;
    GuiTimer guiTimer;
    DisplayFinalTimeTaken displayTimeTakenText;

    GameStatusHandler gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        HUDCanvas = GameObject.FindWithTag("GUI");
        anim = HUDCanvas.GetComponent<Animator>();


        displayTimeTakenText = HUDCanvas.GetComponentInChildren<DisplayFinalTimeTaken>();

        guiTimer = HUDCanvas.GetComponentInChildren<GuiTimer>();

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

            displayTimeTakenText.updateTimeText("Time taken:" + guiTimer.getTimeInHMSFormat());

            anim.SetTrigger("Win");

            StartCoroutine(Wait());

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(10);
    }
}