using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsHandler : MonoBehaviour
{
    [SerializeField]
    private Animator m_Anim;
    [SerializeField]
    private AudioSource m_Sound;

    private int m_Index;
    private bool m_AnimPaused;

    private bool m_ScissorsSelected;

    public void PauseAnim()
    {
        m_Anim.speed = 0;
        m_AnimPaused = true;
    }

    public void ContinueAnim(int i_Index)
    {
        if (m_ScissorsSelected)
        {
            if (i_Index == m_Index && m_AnimPaused && m_Anim.speed == 0)
            {
                m_Anim.speed = 1;
                m_AnimPaused = false;
                m_Sound.Play();
                StartCoroutine(IncreaseCount());
            }
        }
    }

    IEnumerator IncreaseCount()
    {
        yield return new WaitForSeconds(0.15f);
        m_Index++;
    }

    public void ScissorsSelected()
    {
        m_ScissorsSelected = true;
    }
}
