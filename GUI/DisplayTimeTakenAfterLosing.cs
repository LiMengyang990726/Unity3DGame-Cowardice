using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DisplayTimeTakenAfterLosing : MonoBehaviour
{
    Text LoseTimeTaken;

    GameObject timer;

    private double time;

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Timer");
        time = timer.GetComponent<GuiTimer>().time;
        LoseTimeTaken = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        //LoseTimeTaken.text = timer.GetComponent<GuiTimer>().getTimeInHMSFormat();
    }

    public void updateTimeText(string time)
    {
        LoseTimeTaken.text = "Time taken:"+time;
    }

    void LoadScene()
    {
        SceneManager.LoadScene(3);
    }
}