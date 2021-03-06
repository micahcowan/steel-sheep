﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using static SheepUtils;

public class Bumper : MonoBehaviour
{
    public Transform bumperBlocker;
    public VideoPlayer video;

    [Tooltip("How many seconds to take fading in/out")]
    public float fadeTime = 2;
    [Tooltip("How long to display the bumper before fading back out")]
    public float displayTime = 3;

    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = bumperBlocker.GetComponent<MeshRenderer>().material;

        StartCoroutine("FadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetOpacity(float opacity)
    {
        Color color = material.color;
        color.a = opacity;
        material.color = color;
        bumperBlocker.GetComponent<MeshRenderer>().material = material;
    }

    IEnumerator FadeIn()
    {
        float opacity = 1;
        float opacityDelta = -1 / (fadeTime / Time.fixedDeltaTime);
        float countDown = fadeTime;

        Debug.Log("FadeIn");
        while (countDown > 0)
        {
            SetOpacity(opacity);
            yield return new WaitForFixedUpdate();
            countDown -= Time.fixedDeltaTime;
            opacity += opacityDelta;
        }
        SetOpacity(opacity);
        Debug.Log("Display");
        yield return new WaitForSeconds(displayTime);
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        float opacity = 0;
        float opacityDelta = 1 / (fadeTime / Time.fixedDeltaTime);
        float countDown = fadeTime;

        Debug.Log("FadeOut");
        while (countDown > 0)
        {
            SetOpacity(opacity);
            yield return new WaitForFixedUpdate();
            countDown -= Time.fixedDeltaTime;
            opacity += opacityDelta;
        }
        SetOpacity(opacity);

        StartCoroutine("PlayMovie");
    }

    IEnumerator PlayMovie()
    {
        Debug.Log("PlayMovie");
        video.Play();

        while (!video.isPlaying)
            yield return new WaitForFixedUpdate();

        while (video.isPlaying)
        {
            if (Input.GetMouseButtonDown(0))
                break;
            yield return new WaitForFixedUpdate();

        }

        SheepUtils.NextLevel();
    }
}
