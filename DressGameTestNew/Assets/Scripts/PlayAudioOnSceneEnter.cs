using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnSceneEnter : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
