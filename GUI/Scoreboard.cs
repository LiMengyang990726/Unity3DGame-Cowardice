using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scoreboard:MonoBehaviour
{
    public void changeScene(string scenename)
    {
        Application.LoadLevel(scenename);
    }
}
