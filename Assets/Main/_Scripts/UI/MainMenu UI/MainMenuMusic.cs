using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    [SerializeField] AudioClip loadingScreenMusic;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeToLoadingScreenMusic()
    {
        audioSource.clip = loadingScreenMusic;  
        audioSource.Play();
    }
}
