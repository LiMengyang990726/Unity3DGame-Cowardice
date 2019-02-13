using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFinalTimeTaken : MonoBehaviour
{
    Text WinTimeTaken;
    public float time;
    //private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        WinTimeTaken = GetComponent<Text>();
        //audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //WinTimeTaken.text = time + "";
    }

    public void updateTimeText(string time)
    {
        WinTimeTaken.text = time;
    }
   
}
