using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    Text newScore;
    public float time;
    public DisplayFinalTimeTaken displayFinalTimeTaken;
    // Start is called before the first frame update
    void Start()
    {
        time = displayFinalTimeTaken.time;
    }

    // Update is called once per frame
    void Update()
    {
        newScore.text = ""+time;
    }
}
