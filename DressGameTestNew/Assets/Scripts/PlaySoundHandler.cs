using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundHandler : MonoBehaviour
{
    [SerializeField]
    private List<AudioSource> m_Sounds;

    private int m_Count;

    public void PlaySound()
    {
        m_Sounds[m_Count].Play();
        m_Count++;
    }
}
