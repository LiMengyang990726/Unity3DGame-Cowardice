using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVolumn : MonoBehaviour
{
    private AudioSource audio;
    private GameObject obj;
    private float musicVolumn = 1f;

    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("BackgroundMusic");
        audio = obj.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audio.volume = musicVolumn;
    }
    
    private void SetVolumn(float vol)
    {
        musicVolumn = vol;
    }
}
