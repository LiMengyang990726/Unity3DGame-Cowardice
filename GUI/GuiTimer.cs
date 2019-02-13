using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiTimer : MonoBehaviour
{
    public Text timerTxt;

    public double time = 0;

    public double seconds = 0;
    public double minutes = 0;
    public double hours = 0;


    // Start is called before the first frame update
    void Start()
    {
        timerTxt = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //time += Time.deltaTime;
        seconds += Time.deltaTime;
        if (seconds >= 60)
        {
            minutes += 1;
            seconds = 0;
            if (minutes >= 60)
            {
                minutes = 0;
                hours += 1;
            }
        }

        seconds = System.Math.Round(seconds, 2);
        minutes = System.Math.Round(minutes, 2);
        hours = System.Math.Round(hours, 2);



        timerTxt.text = "Time Elasped: " + string.Format("{0:D2}:{1:D2}:{2:D2}", (int)hours, (int)minutes, (int)seconds);



    }

    public string getTimeInHMSFormat()
    {
        string text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)hours, (int)minutes, (int)seconds);
        return text;
    }

    private void LateUpdate()
    {

        
    }
}
