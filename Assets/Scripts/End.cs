using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    bool quit = false;
    void Update() 
    {
        if(!GetComponent<AudioSource>().isPlaying && !quit)
        {
            Application.Quit();
            quit = true;
            Debug.Log("Game over");
        }
    }
}
