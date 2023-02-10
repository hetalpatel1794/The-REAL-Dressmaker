using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAnimationHandler : MonoBehaviour
{
    [SerializeField]
    private Animator m_Anim;
    [SerializeField]
    private AudioSource m_Sound;
    [SerializeField]
    private bool m_PlaySound;

    public void PauseAnimation()
    {
        m_Anim.speed = 0;
    }

    public void PlaySound()
    {
        if (m_PlaySound)
        {
            m_Sound.Play();
        }
    }
}
