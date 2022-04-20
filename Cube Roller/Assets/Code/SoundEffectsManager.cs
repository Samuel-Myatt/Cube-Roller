using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{

    public static AudioClip Land1,Land2;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        Land1 = Resources.Load<AudioClip>("LandingSound");
        Land2 = Resources.Load<AudioClip>("LandingSoundFinal");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(string clip)
    {
        switch (clip)
        {

            case "Land1":
                audioSrc.PlayOneShot(Land1);
                break;
            case "Land2":
                audioSrc.PlayOneShot(Land2);
                break;
            
        }
    }
}