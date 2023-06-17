using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChicken : MonoBehaviour
{
    public static AudioChicken audioChicken;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audioChicken = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio()
    {
        audio.Play();
    }
}
