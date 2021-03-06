﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SheepUtils;

public class WinAreaBehavior : MonoBehaviour
{
    [Tooltip("How long to wait (sheep a-hopping) before loading next level")]
    public float nextLevelWait = 20;

    private bool soundPlayed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayVictorySound()
    {
        if (!soundPlayed)
        {
            GetComponent<AudioSource>().Play();
            soundPlayed = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.main.GetComponent<CameraFollowSheep>().Won();
            theSheepProxy.GetComponent<SheepScript>().StartHopping();
            PlayVictorySound();
            StartCoroutine("DelayedNextScene");
        }
    }

    IEnumerator DelayedNextScene()
    {
        yield return new WaitForSeconds(nextLevelWait);
        SheepUtils.NextLevel();
    }
}
