using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource src;
    private AudioHighPassFilter hpf;
    private AudioChorusFilter chorus;
    private AudioDistortionFilter distort;

    public float leftX = 8;
    public float rightX = 16;
    public Transform player;

    public float speed = 1.2f;
    public float centerFreq = 2500.0f;
    public float distortionLevel = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        hpf = GetComponent<AudioHighPassFilter>();
        chorus = GetComponent<AudioChorusFilter>();
        distort = GetComponent<AudioDistortionFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        float t = 0;
        if (player != null) {
            float x = player.position.x;
            t = Mathf.Clamp((x - leftX) / (rightX - leftX), 0, 1);
        }
        src.pitch = Mathf.Lerp(1, speed, t);
        hpf.cutoffFrequency = Mathf.Lerp(10, centerFreq, t);
        chorus.dryMix = t;
        chorus.wetMix1 = 1 - t;
        chorus.wetMix2 = 1 - t;
        chorus.wetMix3 = 1 - t;
        distort.distortionLevel = Mathf.Lerp(0, distortionLevel, t);
    }
}
