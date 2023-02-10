using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator m_Anim;
    [SerializeField]
    private float m_WaitTime;
    [SerializeField]
    private int m_LoopCount;
    [SerializeField]
    private string m_AnimationToPlay;
    [SerializeField]
    private bool m_UseSound;
    [SerializeField]
    private AudioSource m_SoundObject;
    [SerializeField]
    private bool m_Delay;

    private int m_Count;

    private void Start()
    {
        m_Count = 1;
    }

    public void PlayAnimation()
    {
        if (m_Delay)
        {
            StartCoroutine(PlayAnim(m_WaitTime));
        }

        else
        {
            StartCoroutine(PlayAnim(0));
        }
    }

    IEnumerator PlayAnim(float i_WaitTime)
    {
        yield return new WaitForSeconds(i_WaitTime);
        if (m_Count != m_LoopCount)
        {
            m_Anim.Play(m_AnimationToPlay, 0, 0);
            if (m_UseSound)
            {
                m_SoundObject.Play();
            }
            m_Count++;
        }

        else
        {
            StartCoroutine(Loop());
        }
    }

    IEnumerator Loop()
    {
        if (m_UseSound)
        {
            m_SoundObject.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(m_WaitTime);
        m_Count = 0;
        PlayAnimation();
        
        if (m_UseSound)
        {
            m_SoundObject.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (m_UseSound)
        {
            m_SoundObject.gameObject.SetActive(true);
        }

        m_Count = 0;
    }
}
