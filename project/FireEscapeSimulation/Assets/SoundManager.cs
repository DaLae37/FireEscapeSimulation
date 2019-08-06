using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioSource;
    public AudioClip siren;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void BuzzSiren()
    {
        audioSource.clip = siren;
        audioSource.loop = true;
        audioSource.Play();
    }
}
