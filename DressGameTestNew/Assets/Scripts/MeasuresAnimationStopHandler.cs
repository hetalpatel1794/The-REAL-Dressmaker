using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasuresAnimationStopHandler : MonoBehaviour
{
    [SerializeField]
    private MeasuresHandler m_MeasuresHandler;
    [SerializeField]
    private AudioSource m_Sound;
    [SerializeField]
    private Animator m_Anim;
    [SerializeField]
    private bool m_DisableAnimator;

    private void PauseAnimation()
    {
        m_MeasuresHandler.PauseAnimation();
        if (m_DisableAnimator)
        {
            m_Anim.enabled = false;
        }
    }

    private void MeasuresFinished()
    {
        StartCoroutine(m_MeasuresHandler.MeasuresFinished());
    }

    public void PlaySound()
    {
        if (m_Sound != null)
        {
            m_Sound.Play();
        }
    }
}
